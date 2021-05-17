﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab6
{
    public class Matrix
    {
        private static uint counter = 0;
        private readonly uint id;
        private double[,] m_data;

        public uint Rows    => (uint) m_data.GetLength(0);
        public uint Columns => (uint) m_data.GetLength(1);

        public double this[int i, int j]
        {
            get => m_data[i, j];
            set => m_data[i, j] = value;
        }

        public Matrix()
        {
            id = ++counter;
            m_data = null;
            Console.WriteLine($"Default Constructor for Matrix #{id}");
        }

        public Matrix(uint size = 0, double[,] data = null) : this(size, size, data)
        {
            Console.WriteLine($"NxN constructor for Matrix #{id}");
        }

        public Matrix(uint rows = 0, uint cols = 0, double[,] data = null) : this()
        {
            Console.WriteLine($"NxM constructor for Matrix #{id}");
            if (rows * cols == 0)
            {
                m_data = null;
            }

            m_data = new double[rows, cols];
            if(data != null)
                Array.Copy(data, m_data, data.Length);
        }

        public Matrix(Matrix reference) : this(reference.Rows, reference.Columns,
            reference.m_data)
        {
            Console.WriteLine($"Copy constructor for Matrix #{id}");
        }

        private static bool Multipliable(Matrix A,Matrix B)
        {
            if (A.m_data == null || B.m_data == null)
                return false;
            return (A.Columns == B.Rows);
        }

        private static bool Summarizable(Matrix A, Matrix B)
        {
            if (A.m_data == null || B.m_data == null)
                return false;
            return (A.Columns == B.Columns && A.Rows == B.Rows);
        }
        
        public double Min()
        {
            var min = double.MaxValue;

            foreach (var elem in m_data)
            {
                if (elem < min) min = elem;
            }

            return min;
        }

        public double Max()
        {
            var max = double.MinValue;
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (m_data[i, j] > max)
                        max = m_data[i, j];
                }
            }

            return max;
        }

        //TODO operators +, -, *(matrix, matrix), *(matrix, double)
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (!Summarizable(A, B))
                //TODO add exception
                throw new InvalidOperationException("");
            
            var res = new Matrix(A);
            for (var i = 0; i < res.Rows; i++)
            {
                for (var j = 0; j < res.Columns; j++)
                {
                    res.m_data[i, j] += B.m_data[i, j];
                }
            }

            return res;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (!Summarizable(A, B))
                //TODO add exception
                throw new InvalidOperationException("");
            
            var res = new Matrix(A);
            for (var i = 0; i < res.Rows; i++)
            {
                for (var j = 0; j < res.Columns; j++)
                {
                    res.m_data[i, j] -= B.m_data[i, j];
                }
            }

            return res;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (!Multipliable(A, B))
                throw new InvalidOperationException("");
            
            var res = new Matrix(A);
            for (var i = 0; i < A.Columns; i++)
                for (var j = 0; i < B.Rows; j++)
                    for (var k = 0; k < A.Rows; k++)
                        res.m_data[i, j] += A.m_data[i, k] * B.m_data[k, j];
            
            return res;
        }

        public static Matrix operator *(Matrix A, double B)
        {
            if (A.m_data == null)
                throw new ArgumentNullException("this.m_data is null. Can't multiply");
            
            var res = new Matrix(A);
            for (var i = 0; i < A.Rows; i++)
            {
                for (var j = 0; j < A.Columns; j++)
                {
                    A.m_data[i, j] *= B;
                }
            }

            return res;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                    res.AppendFormat("{0}, ", m_data[i, j]);
                res.Append("\n");
            }
            
            return res.ToString();
        }
    }
}