using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ReceiveServiceBusMessage;

public class ReceiveServiceBusMessageFunc(ILogger<ReceiveServiceBusMessageFunc> logger)
{
	private readonly ILogger<ReceiveServiceBusMessageFunc> _logger = logger;

	[Function(nameof(ReceiveServiceBusMessageFunc))]
	public async Task Run
	(
		[ServiceBusTrigger("test-event-queue", Connection = "ServiceBusConnectionAppSetting")]
		ServiceBusReceivedMessage message,
		ServiceBusMessageActions messageActions
	)
	{
		_logger.LogInformation("Message ID: {id}", message.MessageId);
		_logger.LogInformation("Message Body: {body}", message.Body);
		_logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
		await messageActions.CompleteMessageAsync(message);
	}
}
