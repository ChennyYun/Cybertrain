using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Solver
{
    private List<int[,]> domains;
    private int n;
    private Tilemap map;
    private Vector2 A, B, C, D;

    public Solver(List<int[,]> domains, int n, Tilemap map,Vector2 A, Vector2 B,Vector2 C,Vector2 D)
    {
        this.domains = domains;
        this.n = n;
        this.map = map;
        this.A = A;
        this.B = B;
        this.C = C;
        this.D = D;
        foreach (var domain in domains)
        {
            ShuffleDomain(domain);
        }
    }

    public int[,] Solve(int i, int[,] assignment)
    {
        if (i >= n)
        {
            return assignment; // A solution has been found
        }

        foreach (int j in Enumerable.Range(0, domains[i].GetLength(0)))
        {
            int[,] newAssignment = (int[,])assignment.Clone();
            newAssignment[i, 0] = domains[i][j, 0]; // x coordinate
            newAssignment[i, 1] = domains[i][j, 1]; // y coordinate
            if (CheckConstraints(i, newAssignment))
            {
                int[,] result = Solve(i + 1, newAssignment);
                if (result != null)
                {
                    return result; // A valid solution has been found in recursion
                }
            }
        }
        return null; // No solution was found
    }

    private bool CheckConstraints(int i, int[,] assignment)
    {
        int objectWidth = 2;
        int objectHeight = 2;

        for (int j = 0; j < i; j++)
        {
            // Get the positions
            int x1 = assignment[i, 0];
            int y1 = assignment[i, 1];
            int x2 = assignment[j, 0];
            int y2 = assignment[j, 1];

            // Check for overlaps using Axis-Aligned Bounding Box (AABB) collision detection
            if (Math.Abs(x1 - x2) < objectWidth && Math.Abs(y1 - y2) < objectHeight)
            {
                return false; // Objects are overlapping, so return false.
            }
        }
        Vector2 P = new Vector2(assignment[i,0],assignment[i,1]);
            if (!IsPointInsideParallelogram(P)){
                return false;
            }

        return true;
    }


void ShuffleDomain(int[,] domain)
{
    System.Random rng = new System.Random();
    int n = domain.GetLength(0);
    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);

        // Swap the values at position k and n
        int value1_x = domain[k, 0];
        int value1_y = domain[k, 1];

        domain[k, 0] = domain[n, 0];
        domain[k, 1] = domain[n, 1];

        domain[n, 0] = value1_x;
        domain[n, 1] = value1_y;
    }
}

bool IsPointInTriangle(Vector2 P, Vector2 A, Vector2 B, Vector2 C)
{
    // Barycentric coordinates method
    float alpha = ((B.y - C.y) * (P.x - C.x) + (C.x - B.x) * (P.y - C.y)) /
                  ((B.y - C.y) * (A.x - C.x) + (C.x - B.x) * (A.y - C.y));
    float beta = ((C.y - A.y) * (P.x - C.x) + (A.x - C.x) * (P.y - C.y)) /
                 ((B.y - C.y) * (A.x - C.x) + (C.x - B.x) * (A.y - C.y));
    float gamma = 1.0f - alpha - beta;

    return alpha >= 0 && beta >= 0 && gamma >= 0;
}


bool IsPointInsideParallelogram(Vector2 P)
{
    // Check whether P is inside triangle ABC or triangle ACD
    return IsPointInTriangle(P, A, B, C) || IsPointInTriangle(P, A, C, D);
}


}

