﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsComputabilityProject.Tester
{
    public static class Algorithm
    {
        public static Matrix FindMaximalSubGraph(Matrix A, Matrix B, bool sort = true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            Matrix biggestSubGraph = null;
            int maxCommonEdges = 0;
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                //M.Print();
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix subMatrix = M.GetSubMatrix(x, y, B.VerticesNumber);
                        Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, B);
                        if (commonMatrix.EdgesNumber > maxCommonEdges)
                        {
                            maxCommonEdges = commonMatrix.EdgesNumber;
                            biggestSubGraph = commonMatrix;
                        }
                    }
                }
            }
            return biggestSubGraph;
        }

        public static Matrix FindMinimalSuperGraph(Matrix A, Matrix B, bool sort = true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }

            Matrix SmallestSuperGraph = null;
            int minCommonEdges = int.MaxValue;
            foreach (Matrix M in new IsomorphicGenerator(A))
            {
                for (int x = 0; x <= M.VerticesNumber - B.VerticesNumber; x++)
                {
                    for (int y = 0; y <= M.VerticesNumber - B.VerticesNumber; y++)
                    {
                        Matrix newMatrix = new Matrix(M.Graph);
                        newMatrix.InsertEdgesToMatrixAt(x, y, B);
                        if (newMatrix.EdgesNumber < minCommonEdges)
                        {
                            minCommonEdges = newMatrix.EdgesNumber;
                            SmallestSuperGraph = newMatrix;
                        }
                    }
                }
            }
            return SmallestSuperGraph;
        }

        //Approximate:

        public static Matrix FindMaximalSubGraphApproximate(Matrix A, Matrix B,bool sort=true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            Matrix sortedA = new Matrix(A.Graph);
            Matrix sortedB = new Matrix(B.Graph);
            if (sort)
            {
                sortedA.TransformToSortedForm();
                sortedB.TransformToSortedForm();
            }

            Matrix biggestSubGraph = null;
            int maxCommonEdges = 0;
            for (int x = 0; x <= sortedA.VerticesNumber - sortedB.VerticesNumber; x++)
            {
                for (int y = 0; y <= sortedA.VerticesNumber - sortedB.VerticesNumber; y++)
                {
                    Matrix subMatrix = sortedA.GetSubMatrix(x, y, sortedB.VerticesNumber);
                    Matrix commonMatrix = Matrix.FindCommonMatrix(subMatrix, sortedB);
                    if (commonMatrix.EdgesNumber > maxCommonEdges)
                    {
                        maxCommonEdges = commonMatrix.EdgesNumber;
                        biggestSubGraph = commonMatrix;
                    }
                }

            }
            return biggestSubGraph;
        }

        public static Matrix FindMinimalSuperGraphApproximate(Matrix A, Matrix B,bool sort=true)
        {
            if (A.VerticesNumber < B.VerticesNumber)
            {
                Matrix tmp = B;
                B = A;
                A = tmp;
            }
            Matrix sortedA = new Matrix(A.Graph);
            Matrix sortedB = new Matrix(B.Graph);
            if (sort)
            {
                sortedA.TransformToSortedForm();
                sortedB.TransformToSortedForm();
            }
            Matrix SmallestSuperGraph = null;
            int minCommonEdges = int.MaxValue;
            for (int x = 0; x <= sortedA.VerticesNumber - sortedB.VerticesNumber; x++)
            {
                for (int y = 0; y <= sortedA.VerticesNumber - sortedB.VerticesNumber; y++)
                {
                    Matrix newMatrix = new Matrix(sortedA.Graph);
                    newMatrix.InsertEdgesToMatrixAt(x, y, sortedB);
                    if (newMatrix.EdgesNumber < minCommonEdges)
                    {
                        minCommonEdges = newMatrix.EdgesNumber;
                        SmallestSuperGraph = newMatrix;
                    }
                }
            }
            return SmallestSuperGraph;
        }
    }
}
