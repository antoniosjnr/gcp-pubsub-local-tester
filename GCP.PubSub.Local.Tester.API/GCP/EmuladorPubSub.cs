using Google.Api.Gax;
using Google.Cloud.PubSub.V1;

public class EmuladorPubSub
{
    public string ProjectId
    {
        get
        {
            return Environment.GetEnvironmentVariable("PUBSUB_PROJECT_ID") ?? "project-0";
        }
    }

    public void CreateTopic(string topicId)
    {
        var publisherService = new PublisherServiceApiClientBuilder
        {
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
        }.Build();

        var topicName = new TopicName(ProjectId, topicId);

        publisherService.CreateTopic(topicName);
    }

    public async Task<TopicName> GetTopicName(string topicId)
    {
        var publisherService = await new PublisherServiceApiClientBuilder
        {
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction

        }.BuildAsync();

        var topic = await publisherService.GetTopicAsync($"projects/{ProjectId}/topics/{topicId}");

        return topic.TopicName;
    }

    public async Task CreateSubscriber(string subscriptionId, string topicId, string? pushEndpoint)
    {
        var subscriberService = await new SubscriberServiceApiClientBuilder
        {
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        var topicName = await GetTopicName(topicId);        

        var subscriptionName = new SubscriptionName(ProjectId, subscriptionId);
        subscriberService.CreateSubscription(subscriptionName, topicName, pushConfig: !string.IsNullOrEmpty(pushEndpoint) ? new PushConfig { PushEndpoint = pushEndpoint } : null, ackDeadlineSeconds: 60);
    }


    public async Task PublishMessage(string topicId, string message)
    {
        var topicName = await GetTopicName(topicId);

        var publisher = await new PublisherClientBuilder
        {
            TopicName = topicName,
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        await publisher.PublishAsync(message);
        await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
    }
}