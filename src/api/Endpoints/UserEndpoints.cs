using ERPBridge.DTOs;
using ERPBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERPBridge.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/users");

            group.MapPost("/", CreateUser)
                .WithName("CreateUser")
                .Produces<CreatedResult>(201)
                .Produces(400);

            group.MapGet("/", GetAllUsers)
                .WithName("GetAllUsers")
                .Produces<List<User>>(200);

            group.MapGet("/{id}", GetUserById)
                .WithName("GetUserById")
                .Produces<User>(200)
                .Produces(404);

            group.MapPut("/{id}", UpdateUser)
                .WithName("UpdateUser")
                .Produces<User>(200)
                .Produces(404);

            group.MapDelete("/{id}", DeleteUser)
                .WithName("DeleteUser")
                .Produces(204)
                .Produces(404);
        }

        private static async Task<IResult> CreateUser(
            [FromBody] CreateUserDto userDto, 
            UserService userService)
        {
            var user = await userService.CreateUserAsync(userDto);
            return Results.CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }

        private static async Task<IResult> GetAllUsers(UserService userService)
        {
            var users = await userService.GetAllUsersAsync();
            return Results.Ok(users);
        }

        private static async Task<IResult> GetUserById(int id, UserService userService)
        {
            var user = await userService.GetUserByIdAsync(id);
            return user != null 
                ? Results.Ok(user) 
                : Results.NotFound();
        }

        private static async Task<IResult> UpdateUser(
            int id, 
            [FromBody] UpdateUserDto userDto, 
            UserService userService)
        {
            var updatedUser = await userService.UpdateUserAsync(id, userDto);
            return updatedUser != null 
                ? Results.Ok(updatedUser) 
                : Results.NotFound();
        }

        private static async Task<IResult> DeleteUser(int id, UserService userService)
        {
            var result = await userService.DeleteUserAsync(id);
            return result 
                ? Results.NoContent() 
                : Results.NotFound();
        }
    }
}