using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// 2. Написать приложение, выполняющее парсинг CSV-файла произвольной структуры 
// и сохраняющего его в обычный txt-файл. 
// Все операции проходят в потоках. 
// CSV-файл заведомо имеет большой объем.

namespace Task02
{
    class Program
    {
        static void CreateFile(string path)
        {
            int n = 1000000;
            var r = new Random();

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int i = 0; i < n; i++)
                {
                    streamWriter.Write($"Имя_{i};Фамилия_{i};{r.Next(18, 60)};профессия_{i};\t\n");
                }
            }
        }

        static void Reader(object obj)
        {
            lock (obj)
            {
                List<string> list = (List<string>)obj;
                string path = "data.scv";

                using (StreamReader streamReader = new StreamReader(path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] s = streamReader.ReadLine().Split(';');
                        string str = $"{s[0]} {s[1]} {s[2]} лет {s[3]}";
                        list.Add(str);
                    }
                }
            }
        }

        static void WriterTxt(object obj)
        {
            lock (obj)
            {
                List<string> list = (List<string>)obj;
                string path = "data.txt";


                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    foreach (var item in list)
                        streamWriter.WriteLine(item);
                }
            }
            
        }

        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            //string path = "data.scv";
            //string txtPath = "data.txt";

            //CreateFile(path);
            //Reader(list);
            //WriterTxt(list);

            var reader_thread = new Thread(Reader);
            reader_thread.Name = "Поток считывания";
            reader_thread.Start(list);

                        
            var writer_thread = new Thread(WriterTxt);
            writer_thread.Name = "Поток записи";
            writer_thread.Start(list);








        }
    }
}
