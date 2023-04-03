using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EducationWebApi.Application.Services.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EducationWebApi.Application.Services.Impl.Stroage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;
    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new("DefaultEndpointsProtocol=https;AccountName=alasacademy;AccountKey=bRebku3bLIeGF41AMnYFamHtGabZF11lh1PfXNILHWS2Im6vLhdNUAodN2z3A+rqcN3/FSGfZepb+AStsouPmg==;EndpointSuffix=core.windows.net");
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
        }
        return datas;
    }

    public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string containerName, IFormFile file)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        string fileNewName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
        await blobClient.UploadAsync(file.OpenReadStream());
        return (fileNewName, $"{containerName}/{fileNewName}");
    }
}

