using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Status
    {   
        
        public Status(string watching, string completed, string onHold, string dropped, string planToWatch)
        {
            this.Watching = watching;
            this.Completed = completed;
            this.OnHold = onHold;
            this.Dropped = dropped;
            this.PlanToWatch = planToWatch;
        }

        [DataMember]
        public string Watching { get; }

        [DataMember]
        public string Completed { get; }

        [DataMember(Name ="on_hold")]
        public string OnHold { get; }

        [DataMember]
        public string Dropped { get; }

        [DataMember]
        public string PlanToWatch { get; }
        
        public override string ToString()
        {
            return $"Watching: {Watching}, "
                   + $"Completed: {Completed}, "
                   + $"OnHold: {OnHold}, "
                   + $"Dropped: {Dropped}, "
                   + $"Planned To Watch: {PlanToWatch}"
                ;
        }
    }
}