using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace libmas
{
    public class Matrix
    {
        private Random _rnd = new();
        private int[,] _matrix;
        public int RowLength => _matrix.GetLength(0);
        public int ColumnLength => _matrix.GetLength(1);

        public Matrix(int rows, int cols)
        {
            _matrix = new int[rows, cols];
        }

        public void Fill()
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColumnLength; j++)
                {
                    _matrix[i, j] = _rnd.Next(100);
                }
            }
        }

        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(RowLength);
                sw.WriteLine(ColumnLength);

                for (int i = 0; i < RowLength; i++)
                {
                    for (int j = 0; j < ColumnLength; j++)
                    {
                        sw.WriteLine(_matrix[i, j]);
                    }
                }
            }
        }



        public void Load(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                int rows = Convert.ToInt32(sr.ReadLine());
                int cols = Convert.ToInt32(sr.ReadLine());

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        _matrix[i, j] = Convert.ToInt32(sr.ReadLine());
                    }
                }
            }
        }

        public void Clear()
        {
            _matrix = null;
        }

        public int this[int indr, int indc]
        {
            get
            {
                return _matrix[indr, indc];
            }
            set
            {
                _matrix[indr, indc] = value;
            }

        }

        public DataTable ToDataTable()
        {
            var res = new DataTable();
            for (int i = 0; i < ColumnLength; i++)
            {
                res.Columns.Add("col" + (i + 1));
            }

            for (int i = 0; i < RowLength; i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < ColumnLength; j++)
                {
                    row[j] = _matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }
    }
}
