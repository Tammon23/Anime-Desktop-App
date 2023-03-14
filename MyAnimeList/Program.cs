namespace MyAnimeList;

public class Program
{
    private static async Task Main()
    {
        await MALRequestClient.Init();
        var r = await Anime.GetAnime("Nichijou", limit: 2, fields: "title");
        foreach (var d in r.Data)
        {
            var r2 = await Anime.GetAnimeDetails(d.Node.Id);
            if (r2 == null)
                Console.WriteLine("r2 is null");
            else
                Console.WriteLine(r2);
            //Console.WriteLine(r2);
        }

        Console.WriteLine(r == null);
        Console.WriteLine(r);

        // initializing variables for first time run        
        /*await MALRequestClient.Init();

        Forum forum = new();
        AnimeListStatus al = new(animeStatus: AnimeStatusEnum.PlanToWatch);
        /*StatusEnum.TryParse()#1#
        var result = await forum.GetForumBoards();
        Console.WriteLine(result==null);
        Console.WriteLine(result);*/
    }
}