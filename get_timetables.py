from datetime import datetime, timezone
from typing import Union, Dict, List
import logging.handlers
from lxml import etree
import requests
import logging
import json
import re


def get_timetable_from_web():
    url = "https://animeschedule.net/"
    r = requests.get(url)
    return etree.HTML(r.text)


def get_timetables(html) -> List[Dict[str, str]]:
    seen = set()
    result = []
    show_result = {}
    day_divs = html.xpath("//div[contains(@class, 'timetable-column ')]")
    day_of_week = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
    for day in day_divs:
        current_class = day.get("class")

        for check_day in day_of_week:
            if check_day in current_class:

                for show in day.xpath("//div[contains(@class, 'timetable-column-show ')]"):
                    inner = show.xpath(".//h3[@class='time-bar']")[0]
                    language = inner.xpath(".//span")[1].text
                    if language != "SUB" and language != "DUB":
                        continue

                    if (show_id := show.get('showid'), language) in seen:
                        continue
                    else:
                        seen.add((show_id, language))

                    show_result["language"] = language

                    show_result["raw_name"] = show.get('route')
                    show_result["clean_name"] = show.xpath(".//a/h2")[0].text
                    show_result["episode"] = re.search(r'\d+', inner.xpath(".//span")[0].text).group()
                    show_result["datetime"] = inner.xpath(".//time")[0].get("datetime")

                    result.append(show_result.copy())
                    show_result.clear()

    return result


def get_mal_id(raw_name: str) -> Union[None, str]:
    url = f"https://animeschedule.net/anime/{raw_name}"
    show_site = requests.get(url).text
    res = etree.HTML(show_site)
    try:
        return res.xpath("//a[@class='anime-link myanimelist-link']")[0].get("href").split("/")[4]
    except:
        return None


def save_result(result: List[Dict[str, str]], filename: str = "anime_weekly_timetable.json",
                pretty_print: bool = True) -> None:
    created_at = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S.%f%z")
    result = {"created_at": created_at, "timetable": result}
    with open(filename, "w") as write_file:
        if pretty_print:
            json.dump(result, write_file, indent=4)
        else:
            json.dump(result, write_file)


if __name__ == '__main__':
    logger = logging.getLogger(__name__)
    logger.setLevel(logging.DEBUG)
    logger_file_handler = logging.handlers.RotatingFileHandler(
        "status.log",
        maxBytes=1024 * 1024,
        backupCount=1,
        encoding="utf8",
    )
    formatter = logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s")
    logger_file_handler.setFormatter(formatter)
    logger.addHandler(logger_file_handler)

    logger.info("Starting action")
    tree = get_timetable_from_web()
    all_shows = get_timetables(tree)

    num_anime = 0

    for i, show in enumerate(all_shows):
        num_anime += 1
        if (mal_id := get_mal_id(show['raw_name'])) is not None:
            all_shows[i]["mal_id"] = mal_id

    logger.info(f"Processed {num_anime} shows")

    save_result(all_shows, pretty_print=False)
    logger.info("Finished updating json")
