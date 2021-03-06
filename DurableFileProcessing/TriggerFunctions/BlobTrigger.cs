﻿using DurableFileProcessing.Orchestrators;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DurableFileProcessing.TriggerFunctions
{
    [StorageAccount("FileProcessingStorage")]
    public class BlobTrigger
    {
        [FunctionName("FileProcessing_BlobTrigger")]
        public static async Task BlobTriggerStart([BlobTrigger("original-store/{name}")] CloudBlockBlob myBlob, string name, [DurableClient] IDurableOrchestrationClient starter, ILogger log)
        {
            string instanceId = await starter.StartNewAsync(nameof(FileProcessingOrchestrator), input: name);

            log.LogInformation($"Started orchestration with ID = '{instanceId}', Blob '{name}'.");
        }
    }
}