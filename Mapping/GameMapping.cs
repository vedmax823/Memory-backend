using System;
using Memory.DTOs;
using Memory.DTOs.cardDtos;
using Memory.DTOs.User;
using Memory.Entities;

namespace Memory.Mapping;

public static class GameMapping
{

    public static Game ToEntity(this CreateGameDto gameDto, User user)
    {
        var size = FindClosestFactors(gameDto.CardsCount);
        var grid = CreateField(size.Item2, size.Item1);

        Game game = new()
        {
            MaxPlayersCount = gameDto.MaxPlayersCount,
            CardsCount = gameDto.CardsCount,
            Cols = size.Item2,
            Rows = size.Item1,
            Field = grid,
            TurnUser = user.Id
        };

        game.Users.Add(user);
        game.IsStarted = game.MaxPlayersCount == 1 ? true : false;
        return game;
    }

    public static UserDto ToUserdto(this User user)
    {
        return new(
            user.Id,
            user.Name,
            user.UserNumber
        );
    }

    public static GameDto ToGameDto(this Game game)
    {
        return new(
            game.Id,
            game.Rows,
            game.Cols,
            CreateCardsDtoList(game.Field),
            game.CardsCount,
            game.MaxPlayersCount,
            MakeUserListDto(game.Users),
            game.TurnUser,
            game.IsStarted,
            game.OpenCards
        );
    }


    private static List<Card> CreateField(int rows, int cols)
    {
        
        int totalCards = rows * cols;
        int uniqueImages = totalCards / 2;
        var images = new List<string>();

        // Generate image names like p1.jpg, p2.jpg, ..., p[uniqueImages].jpg
        for (int i = 1; i <= uniqueImages; i++)
        {
            images.Add($"p{i}.jpg");
        }

        // Duplicate images to match the total number of cards
        var cardsImages = new List<string>();
        foreach (var img in images)
        {
            cardsImages.Add(img);
            cardsImages.Add(img);
        }

        // Shuffle the list of images randomly
        var random = new Random();
        cardsImages = cardsImages.OrderBy(x => random.Next()).ToList();
        int index = 0;
        var fields = new List<Card>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                fields.Add(new Card
                {
                    Row = i,
                    Col = j,
                    Link = $"images/{cardsImages[index]}",
                    IsOpen = false
                });
                index++;
            }
        }

        return fields;
    }

    private static List<CardDto> CreateCardsDtoList(this List<Card> field)
    {
        var fields = new List<CardDto>();

        foreach (Card card in field)
        {
            fields.Add(new CardDto(
                card.Id,           // Guid Id
                card.Col,          // int Col
                card.Row,          // int Row
                card.IsOpen,       // bool IsOpen
                card.IsOpen ? card.Link : ""  // string Link, with a conditional check
            ));
        }

        return fields;
    }

    static Tuple<int, int> FindClosestFactors(int number)
    {
        int factor1 = 1;
        int factor2 = number;

        for (int i = 1; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                int potentialFactor1 = i;
                int potentialFactor2 = number / i;

                if (Math.Abs(potentialFactor1 - potentialFactor2) < Math.Abs(factor1 - factor2))
                {
                    factor1 = potentialFactor1;
                    factor2 = potentialFactor2;
                }
            }
        }

        return Tuple.Create(factor1, factor2);
    }

    private static List<UserDto> MakeUserListDto(List<User> users)
    {
        var userDTOs = new List<UserDto>();

        foreach (User user in users)
        {
            userDTOs.Add(new UserDto(
                user.Id,
                user.Name,
                user.UserNumber
            ));
        }

        return userDTOs;

    }



}
