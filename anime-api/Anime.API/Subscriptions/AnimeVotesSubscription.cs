using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;

namespace Anime.API.Subscriptions;

public interface IDatabaseSubscription
{
    void SubscribeTableDependency(string connectionString);
}

public class AnimeVotesSubscription : IDatabaseSubscription
{
    SqlTableDependency<Common.Entities.AnimeVotes> _animeVotesTableDependency;

    public AnimeVotesSubscription()
    {

    }

    public void SubscribeTableDependency(string connectionString)
    {
        _animeVotesTableDependency = new SqlTableDependency<Common.Entities.AnimeVotes>(connectionString, null, null, null, null, null, DmlTriggerType.All, false);        
        _animeVotesTableDependency.OnChanged += AnimeVotesTableDependency_OnChanged;
        _animeVotesTableDependency.OnError += AnimeVotesTableDependency_OnError;
        _animeVotesTableDependency.Start();
    }

    private void AnimeVotesTableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Common.Entities.AnimeVotes> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            var animeVote = e.Entity;

            // Broadcast to All 

        }
    }

    private void AnimeVotesTableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        throw new NotImplementedException();
    }
}