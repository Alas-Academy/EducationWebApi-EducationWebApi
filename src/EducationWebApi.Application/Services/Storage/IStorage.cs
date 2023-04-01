﻿
using Microsoft.AspNetCore.Http;

namespace EducationWebApi.Application.Services.Storage;

public interface IStorage
{
    Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
    Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile file);
    Task DeleteAsync(string pathOrContainerName, string fileName);
    List<string> GetFiles(string pathOrContainerName);
    bool HasFile(string pathOrContainerName, string fileName);
}