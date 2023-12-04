namespace ManualMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string root = Directory.GetCurrentDirectory();
            string html = "";
            using (StreamReader sr = new StreamReader(Path.Combine(root, "html.txt")))
            {
                html = sr.ReadToEnd();
                sr.Close();
            }
            if (!File.Exists(Path.Combine(root, "html2.txt")))
            {
                File.Create(Path.Combine(root, "html2.txt"));
            }
            using(StreamWriter sw = new StreamWriter(Path.Combine(root, "html2.txt")))
            {
                sw.Write(HtmlToCshtml(html));
                sw.Close();
            }
            Console.WriteLine("SUCCESS");
            while (true);
        }
        public static string HtmlToCshtml(string html)
        {
            string cshtml = "";

            for (int i = 0; i < html.Length; i++)
            {
                //if (html[i] == '\n') cshtml += "\r";
                cshtml += html[i];

                // http://.css
                // http://.html

                // g.css -> ~/g.css
                // g.html -> g

                if (cshtml.EndsWith("href=") || cshtml.EndsWith("src="))
                {
                    cshtml += html[++i];
                    string temp = "" + html[++i];

                    while (temp[^1] != cshtml[^1])
                    {
                        temp += html[++i];
                    }

                    if (!temp.Contains("://"))
                    {
                        if (temp.EndsWith($".html{cshtml[^1]}")) temp = temp.Substring(0, temp.Length - 6) + cshtml[^1];
                        else if (!(temp.Contains("~/") || temp[0] == '#')) temp = "~/" + temp;
                    }

                    cshtml += temp;
                }
            }

            return cshtml;
        }
    }
}