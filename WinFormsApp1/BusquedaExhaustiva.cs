using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Node
    {
        public Position position;
        public List<Position> path;

        public Node(Position position, List<Position> path)
        {
            this.position = position;
            this.path = new List<Position>(path);
        }
    }

    class ExhaustiveSearch
    {
        private int[,] grid;
        private Position initialPosition;
        private Position targetPosition;

        public ExhaustiveSearch(int[,] grid, Position initialPosition, Position targetPosition)
        {
            this.grid = grid;
            this.initialPosition = initialPosition;
            this.targetPosition = targetPosition;
        }

        public List<Position> FindSolution()
        {
            Queue<Node> queue = new Queue<Node>();
            Node startNode = new Node(initialPosition, new List<Position>());
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();
                Position currentPosition = currentNode.position;
                List<Position> currentPath = currentNode.path;

                if (currentPosition.x == targetPosition.x && currentPosition.y == targetPosition.y)
                {
                    return currentPath;
                }

                List<Position> newPositions = GenerateNewPositions(currentPosition);
                foreach (Position newPosition in newPositions)
                {
                    List<Position> newPath = new List<Position>(currentPath);
                    newPath.Add(newPosition);

                    Node newNode = new Node(newPosition, newPath);
                    queue.Enqueue(newNode);
                }
            }

            return null; // No hay solution
        }

        private List<Position> GenerateNewPositions(Position currentPosition)
        {
            List<Position> newPositions = new List<Position>();

            // Verifica movimientos válidos y agrega nuevas posiciones
            if (currentPosition.y > 0)
            {
                newPositions.Add(new Position(currentPosition.x, currentPosition.y - 1));
            }

            if (currentPosition.y < grid.GetLength(1) - 1)
            {
                newPositions.Add(new Position(currentPosition.x, currentPosition.y + 1));
            }

            if (currentPosition.x > 0)
            {
                newPositions.Add(new Position(currentPosition.x - 1, currentPosition.y));
            }

            if (currentPosition.x < grid.GetLength(0) - 1)
            {
                newPositions.Add(new Position(currentPosition.x + 1, currentPosition.y));
            }

            // Busca obstáculos en la cuadrícula
            foreach (Position newPosition in newPositions.ToList()) // Usa ToList() para crear una copia
            {
                if (grid[newPosition.x, newPosition.y] == 1) // Obstáculo
                {
                    newPositions.Remove(newPosition);
                }
            }
            return newPositions;
        }
    }
}