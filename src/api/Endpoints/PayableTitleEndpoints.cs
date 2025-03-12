using api.Models;
using ERPBridge.DTOs;
using ERPBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERPBridge.Endpoints
{
    public static class PayableTitleEndpoints
    {
        public static void MapPayableTitleEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/payable-titles");

            group.MapPost("/", CreatePayableTitle)
                .WithName("CreatePayableTitle")
                .Produces<CreatedResult>(201)
                .Produces<ProblemDetails>(400);

            group.MapGet("/", GetAllPayableTitles)
                .WithName("GetAllPayableTitles")
                .Produces<List<PayableTitle>>(200);

            group.MapGet("/{id}", GetPayableTitleById)
                .WithName("GetPayableTitleById")
                .Produces<PayableTitle>(200)
                .Produces(404);

            group.MapPut("/{id}", UpdatePayableTitle)
                .WithName("UpdatePayableTitle")
                .Produces<PayableTitle>(200)
                .Produces<ProblemDetails>(400)
                .Produces(404);

            group.MapDelete("/{id}", DeletePayableTitle)
                .WithName("DeletePayableTitle")
                .Produces(204)
                .Produces(404);
        }

        private static async Task<IResult> CreatePayableTitle(
            [FromBody] CreatePayableTitleDto payableTitleDto,
            PayableTitleService payableTitleService)
        {
            var (payableTitle, errorMessage) = await payableTitleService.CreatePayableTitleAsync(payableTitleDto);

            if (errorMessage != null)
            {
                return Results.BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = errorMessage,
                    Status = StatusCodes.Status400BadRequest
                });
            }

            return Results.CreatedAtRoute("GetPayableTitleById", new { id = payableTitle!.Id }, payableTitle);
        }

        private static async Task<IResult> GetAllPayableTitles(PayableTitleService payableTitleService)
        {
            var payableTitles = await payableTitleService.GetAllPayableTitlesAsync();
            return Results.Ok(payableTitles);
        }

        private static async Task<IResult> GetPayableTitleById(int id, PayableTitleService payableTitleService)
        {
            var payableTitle = await payableTitleService.GetPayableTitleByIdAsync(id);
            return payableTitle != null
                ? Results.Ok(payableTitle)
                : Results.NotFound();
        }

        private static async Task<IResult> UpdatePayableTitle(
            int id,
            [FromBody] UpdatePayableTitleDto payableTitleDto,
            PayableTitleService payableTitleService)
        {
            var (updatedPayableTitle, errorMessage) = await payableTitleService.UpdatePayableTitleAsync(id, payableTitleDto);

            if (errorMessage != null)
            {
                return Results.BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = errorMessage,
                    Status = StatusCodes.Status400BadRequest
                });
            }

            return updatedPayableTitle != null
                ? Results.Ok(updatedPayableTitle)
                : Results.NotFound();
        }

        private static async Task<IResult> DeletePayableTitle(int id, PayableTitleService payableTitleService)
        {
            var result = await payableTitleService.DeletePayableTitleAsync(id);
            return result
                ? Results.NoContent()
                : Results.NotFound();
        }
    }
}
