using API.Dtos.Algorithm;
using API.Dtos.Request;
using API.Dtos.Visualizations;
using API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Endpoints;

public static class EndPoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) => 
            TypedResults.Ok(await authService.LoginUser(dto)));

        endpoints.MapPost("api/signup", async (RegisterDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.RegisterAsync(dto)));

        // User Apis
        endpoints.MapGet("api/user", () => async () => TypedResults.Ok() );

        // Visualization Apis
        endpoints.MapGet("api/visualization", async (IVisualizationService service, [AsParameters] VisualizationFilters filters, ClaimsPrincipal user)
            => TypedResults.Ok(await service.GetVisualizations(filters, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));
        
        endpoints.MapPost("api/visualization", 
            [Authorize] async (VisualizationCreateDto dto , IVisualizationService service, ClaimsPrincipal user) 
            => TypedResults.Ok(await service.CreateVisualization(dto, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));

        endpoints.MapPost("api/visualization/{id}/like",
            [Authorize] async (int id, IVisualizationService service, ClaimsPrincipal user)
            => TypedResults.Ok(await service.LikeVisualization(id, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));

        endpoints.MapPost("api/visualization/{id}",
            [Authorize] async (int id, IVisualizationService service, ClaimsPrincipal user)
            => TypedResults.Ok(await service.LikeVisualization(id, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));

        endpoints.MapGet("api/visualization/{id}", async (IVisualizationService service, int id, ClaimsPrincipal user) 
            => TypedResults.Ok(await service.GetVisualization(id, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));

        // Algorithm endpoints
        endpoints.MapGet("api/algorithm", async (IAlgorithmService algorithmService) =>
            TypedResults.Ok(await algorithmService.GetAlgorithms()));

        endpoints.MapPost("api/algorithm", [Authorize] async (AlgorithmCreateDto dto, IAlgorithmService algorithmService, ClaimsPrincipal user) =>
            TypedResults.Ok(await algorithmService.CreateDto(dto)));

        // Tags endpoints
        endpoints.MapGet("api/tags", async (ITagService tagService) => TypedResults.Ok(await tagService.GetTags()));

        // Course endpoints
        // endpoints.MapGet("api/courses", async (ICourseService courseService) => TypedResults.Ok(await courseService.GetCourses()));

        return endpoints;
    }

}
