import logging
from typing import List
import json as json_

from animdl.core.cli import helpers
from animdl.core.cli.helpers import get_check, process_query
from animdl.core.cli.helpers.searcher import link
from animdl.core.cli.http_client import client
from animdl.core.config import DEFAULT_PROVIDER
from animdl.core.codebase import providers


def get_providers() -> List[str]:
    """
    Gets the names of all available anime content providers to get from
    :return:
    """
    return link.keys()


def search(query: str, provider: str = DEFAULT_PROVIDER, **kwargs) -> List[str]:
    """
    Searches for anime using the animdl lib
    :param query: The search query, cannot be null, i.e. "naruto"
    :param provider: the string name of the available providers
    :param kwargs: Exists just to consume
    :return:
    """
    return list(link.get(provider)(client, query))


def search_with_generator(query: str, provider: str = DEFAULT_PROVIDER, **kwargs) -> None:
    """
    Searches for anime using the animdl and returns a generator
    :param query: The search query, cannot be null, i.e. "naruto"
    :param provider: the string name of the available providers
    :param kwargs: Exists just to consume
    :return:
    """

    return link.get(provider)(client, query)

# def grab2(query: str, auto, index, log_level, **kwargs) -> None:
#     """
#     Stream the stream links to the stdout stream for external usage.
#     :param query:
#     :param auto:
#     :param index:
#     :param log_level:
#     :param kwargs:
#     :return:
#     """
#     r = kwargs.get("range") # the range of episodes to query, probably not needed
#     res = []
#     session = client
#     anime, provider = process_query(session, query, logger, auto=auto, auto_index=index)
#     if not anime:
#         return
#
#     for stream_url_caller, episode in providers.get_appropriate(
#             session, anime.get("anime_url"), check=get_check(r)
#     ):
#         stream_url = list(helpers.ensure_extraction(session, stream_url_caller))
#         res.append(json_.dumps({"episode": episode, "streams": stream_url}))


# def animdl_grab(query, file, auto, index, log_level, **kwargs):
#
#     r = kwargs.get("range")
#
#     session = client
#     logger = logging.getLogger("grabber")
#     anime, provider = process_query(session, query, logger, auto=auto, auto_index=index)
#     if not anime:
#         return
#     logger.name = "{}/{}".format(provider, logger.name)
#     logger.info("Initializing grabbing session.")
#
#     if file:
#         collected_streams = []
#         file += ".json" if not file.endswith(".json") else ""
#
#     for stream_url_caller, episode in providers.get_appropriate(
#         session, anime.get("anime_url"), check=get_check(r)
#     ):
#         stream_url = list(helpers.ensure_extraction(session, stream_url_caller)) # <----------
#
#         if file:
#             collected_streams.append({"episode": episode, "streams": stream_url})
#
#         if file:
#             logger.info("{} => {!r}".format("E%02d" % episode, file))
#             try:
#                 with open(file, "w") as json_file_writer:
#                     json.dump(collected_streams, json_file_writer, indent=4)
#             except WindowsError:
#                 logger.error(
#                     "Failed to attempt I/O on the file at the moment; the unwritten value(s) will be written in the next I/O."
#                 )
#         else:
#             to_stdout(
#                 json.dumps({"episode": episode, "streams": stream_url}),
#                 ("E%02d" % episode) if log_level <= 20 else "",
#             )
#     logger.info("Grabbing session complete.")

# def grab(query, episode):
#     session = client
#     an
# def grab_from_url(query, file, auto, index, log_level, **kwargs):
#
#     r = kwargs.get("range")
#
#     session = client
#     logger = logging.getLogger("grabber")
#     # anime = {anime url, anime name (from query)} , provider -> search method
#     anime, provider = process_query(session, query, logger, auto=auto, auto_index=index)
#     if not anime:
#         return
#     logger.name = "{}/{}".format(provider, logger.name)
#     logger.info("Initializing grabbing session.")
#
#     if file:
#         collected_streams = []
#         file += ".json" if not file.endswith(".json") else ""
#
#     for stream_url_caller, episode in providers.get_appropriate(
#         session, anime.get("anime_url"), check=get_check(r)
#     ):
#         stream_url = list(helpers.ensure_extraction(session, stream_url_caller))
#
#         if file:
#             collected_streams.append({"episode": episode, "streams": stream_url})
#
#         if file:
#             logger.info("{} => {!r}".format("E%02d" % episode, file))
#             try:
#                 with open(file, "w") as json_file_writer:
#                     json_.dump(collected_streams, json_file_writer, indent=4)
#             except WindowsError:
#                 logger.error(
#                     "Failed to attempt I/O on the file at the moment; the unwritten value(s) will be written in the next I/O."
#                 )
#         else:
#             from animdl.core.cli.helpers import to_stdout
#             to_stdout(
#                 json_.dumps({"episode": episode, "streams": stream_url}),
#                 ("E%02d" % episode) if log_level <= 20 else "",
#             )
#     logger.info("Grabbing session complete.")
#
# # auto = true, index=episode number
# animdl_grab(query="9anime:broken blade", file="", auto=False, index=0, log_level=20)

for result in search_with_generator("ascendance of a bookworm", "9anime"):
    print(result)