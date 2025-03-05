using API.Dtos;
using API.Dtos.Algorithm;
using API.Dtos.Visualizations;
using API.Persistence.Entities;
using API.Persistence.UnitOfWork;
using API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementation;

public class VisualizationService(IUnitOfWork wof) : IVisualizationService
{
    private readonly IUnitOfWork _wof = wof;

    public async Task<ResultDto> CreateVisualization(VisualizationCreateDto dto, int userId)
    {
        var algo = await _wof.Algorithms.FindAsync(dto.AlgorithmId);
        if(algo is null) return new ResultDto(false, "Algorithm not found");

        var user = await _wof.Users.FindAsync(userId);
        if (user is null) return new ResultDto(false, "User not found");

        var visualization = new Visualization
        {
            Algorithm = algo,
            Title = dto.Title,
            User = user,
            Js = dto.Js,
            Html = dto.Html,
            Css = dto.Css,
            CreatedAt = DateTime.Now,
        };

        await _wof.Visualizations.InsertAsync(visualization);
        await _wof.SaveChangesAsync();
        return new ResultDto(true, "Visualization created successfully");
    }
    public async Task<ResultWithDataDto<VisualizationDto>> GetVisualization(int id, int userId=0 )
    {
        var que = _wof.Visualizations.GetQueryable();
        var visualization = await que
            .Include(x => x.User)
            .Include(x => x.Votes)
            .Include(x => x.Algorithm)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (visualization is null) return new ResultWithDataDto<VisualizationDto>(false, default, "Visualization not found");
        var dto = new VisualizationDto
        {
            Id = visualization.Id,
            Title = visualization.Title,
            UserId = visualization.User.Id,
            UserName = visualization.User.Username,
            Js = visualization.Js,
            Css = visualization.Css,
            Html = visualization.Html,
            VoteCount = visualization.Votes.Count,
            IsVoted = visualization.Votes.Any(x => x.UserId == userId),
            Views = visualization.Views,
            Algorithm = visualization.Algorithm.Title,
        };
        visualization.Views++;
        await _wof.SaveChangesAsync();
        return new ResultWithDataDto<VisualizationDto>(true, dto, null);
    }
    public async Task<ResultWithDataDto<List<VisualizationDto>>> GetVisualizations(VisualizationFilters filters, int userId=0)
    {
        
        var query = _wof.Visualizations.GetQueryable();

        if (filters.FromDate.HasValue) query = query.Where(x => x.CreatedAt >= filters.FromDate);
        if (filters.ToDate.HasValue) query = query.Where(x => x.CreatedAt <= filters.FromDate);
        if (filters.LikeGreaterThan.HasValue) query = query.Where(x => x.Votes.Count >= filters.LikeGreaterThan);
        if (filters.LikeLessThan.HasValue) query = query.Where(x => x.Votes.Count <= filters.LikeGreaterThan);
        if (filters.ViewCountGreaterThan.HasValue) query = query.Where(x => x.Views >= filters.ViewCountGreaterThan);
        if (filters.ViewCountLessThan.HasValue) query = query.Where(x => x.Views <= filters.ViewCountLessThan);
        if (filters.AlgorithmId.HasValue) query = query.Where(x => x.AlgorithmId == filters.AlgorithmId);
        if (filters.IsViewsDecending.HasValue) query = query.OrderByDescending(x => x.Views);
        if (filters.IsVoteDecending.HasValue) query = query.OrderByDescending(x => x.Votes.Count);


        var res = await query.Select(x => new VisualizationDto
        {
            Id = x.Id,
            Title = x.Title,
            UserId = x.User.Id,
            UserName = x.User.Username,
            Js = x.Js,
            Css = x.Css,
            Html = x.Html,
            VoteCount = x.Votes.Count,
            Views = x.Views,
            Algorithm = x.Algorithm.Title,
            IsVoted = x.Votes.Any(x => x.UserId == userId),
            TrendScore = (x.Views / 15000) + (x.Votes.Count * 200) + (DateTime.Now - x.CreatedAt).Days / 50,
        }).ToListAsync();
        if (filters.IsTrending.HasValue) res = res.OrderByDescending(x => x.TrendScore).ToList();

        return new ResultWithDataDto<List<VisualizationDto>>(true, res, null);
    }

    public async Task<ResultDto> LikeVisualization(int visId, int userId)
    {
        var vis = await _wof.Visualizations.FindAsync(visId);
        if (vis is null) return new ResultDto(false, "Visulization not found");

        var user = await _wof.Users.FindAsync(userId);
        if (user is null) return new ResultDto(false, "User not found");

        var vote = vis.Votes.FirstOrDefault(x => x.UserId == userId);
        if(vote is null)
        {
            vis.Votes.Add(new Vote { User = user, });
        } else
        {
            vis.Votes.Remove(vote);
        }
        await _wof.SaveChangesAsync();
        return new ResultDto(true, "Vote added successfully");
    }

}
