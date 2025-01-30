var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Article>("article");
builder.AddProject<Projects.ArticleRead>("article-read");
builder.AddProject<Projects.Comment>("comment");
builder.AddProject<Projects.HotArticle>("hot-article");
builder.AddProject<Projects.Like>("like");
builder.AddProject<Projects.View>("view");

builder.Build().Run();
