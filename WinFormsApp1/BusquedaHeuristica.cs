using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medallion.Collections;


namespace WinFormsApp1
{
    public class BusquedaHeuristica
    {
        private int[,] grid;
        private WinFormsApp1.Position initialPosition;
        private WinFormsApp1.Position targetPosition;

        public BusquedaHeuristica(int[,] grid, WinFormsApp1.Position initialPosition, WinFormsApp1.Position targetPosition)
        {
            this.grid = grid;
            this.initialPosition = initialPosition;
            this.targetPosition = targetPosition;
        }

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
            public int Gscore; // Costo de movimiento real desde la posición inicial
            public int Hscore; // Estimación heurística a la posición objetivo
            public int Fscore { get { return Gscore + Hscore; } } // Puntaje total

            public Node(Position position, List<Position> path, int gscore, int hscore)
            {
                this.position = position;
                this.path = path;
                this.Gscore = gscore;
                this.Hscore = hscore;
            }
        }

       public class BestFirstSearch
        {
            private int[,] grid;
            private Position initialPosition;
            private Position targetPosition;

            public BestFirstSearch(int[,] grid, Position initialPosition, Position targetPosition)
            {
                this.grid = grid;
                this.initialPosition = initialPosition;
                this.targetPosition = targetPosition;
            }

            public List<Position> FindSolution()
            {
                // Función de heurística: distancia Manhattan
                Func<Position, int> heuristic = (pos) => Math.Abs(pos.x - targetPosition.x) + Math.Abs(pos.y - targetPosition.y);

                // Cola con prioridad para nodos ordenada por Fscore
                PriorityQueue<Node> queue = new PriorityQueue<Node>(); // Ordenar por Fscore (heurística)

                Node startNode = new Node(initialPosition, new List<Position>(), 0, heuristic(initialPosition));
                queue.Enqueue(startNode);

                while (queue.Count > 0)
                {
                    Node currentNode = queue.Dequeue();
                    Position currentPosition = currentNode.position;
                    List<Position> currentPath = currentNode.path;
                    int currentGScore = currentNode.Gscore;

                    if (currentPosition.x == targetPosition.x && currentPosition.y == targetPosition.y)
                    {
                        return currentPath;
                    }

                    List<Position> newPositions = GenerateNewPositions(currentPosition);
                    foreach (Position newPosition in newPositions)
                    {
                        List<Position> newPath = new List<Position>(currentPath);
                        newPath.Add(newPosition);

                        int newGScore = currentGScore + CalculateMovementCost(currentPosition, newPosition); // Función de costo de movimiento

                        Node newNode = new Node(newPosition, newPath, newGScore, heuristic(newPosition));
                        queue.Enqueue(newNode);
                    }
                }

                return null; // No se encontró solución
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
                foreach (Position newPosition in newPositions.ToList())
                { // Usa ToList() para crear una copia
                    if (grid[newPosition.x, newPosition.y] == 1)
                    { // Obstáculo
                        newPositions.Remove(newPosition);
                    }
                }
                return newPositions;
            }
            private int CalculateMovementCost(Position posicionActual, Position nuevaPosicion)
            {
                int deltaX = Math.Abs(posicionActual.x - nuevaPosicion.x);
                int deltaY = Math.Abs(posicionActual.y - nuevaPosicion.y);
                return deltaX + deltaY;
            }
        }
    }
}