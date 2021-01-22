using System;
using System.Threading;
using System.Threading.Tasks;

using Azure.Storage.Blobs;

using MediatR;

using Microsoft.Extensions.Options;

using Waitress.Configuration;
using Waitress.Events;

namespace Waitress.Handlers
{
    public class ReceiptNotificationHandler : INotificationHandler<OrderPlacedMessageReceived>
    {
        private readonly BlobContainerClient _blobContainerClient;

        public ReceiptNotificationHandler(BlobServiceClient blobServiceClient, IOptions<BlobStorageSettings> blobStorageSettingsOptions)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageSettingsOptions.Value.ContainerName);
        }

        public async Task Handle(OrderPlacedMessageReceived notification, CancellationToken cancellationToken)
        {
            var fileName = $"receipt-{DateTime.Now:yyyyMMddHHmmss}.csv";
            await _blobContainerClient.UploadBlobAsync(fileName, notification.AsCsvStringStreamForBlobStorage(), cancellationToken);

        }
    }
}