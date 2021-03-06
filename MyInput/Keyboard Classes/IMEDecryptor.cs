using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Compression;

namespace MyInput.Keyboard_Classes
{
    public class KeyboardIME
    {
        public KeyboardIME(string path)
        {
            this.name = path;
            path = "Layouts\\" + path + ".dll";
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                DeflateStream dfs = new DeflateStream(fs, CompressionMode.Decompress);
                StreamReader sr = new StreamReader(dfs, Encoding.Unicode);
                al = new ArrayList();
                string tempkey = "";
                int state = 0;
                IMEData imd = new IMEData();
                while (!sr.EndOfStream)
                {
                    int i = sr.Read();
                    char ccc = (char)i;
                    switch (state)
                    {
                        case 0:
                            if (i != (int)'\t')
                            {
                                tempkey += (char)i;
                            }
                            else
                            {
                                state = 1;
                                imd.key = tempkey;
                                tempkey = "";
                                imd.Values = new ArrayList();
                            }
                            break;
                        case 1:
                            if (i != (int)'\t' && i != (int)';')
                            {
                                tempkey += (char)i;
                            }
                            else if (i == (int)';')
                            {
                                imd.Values.Add(tempkey);
                                tempkey = "";
                            }
                            else if (i == (int)'\t')
                            {
                                al.Add(imd);
                                imd = new IMEData();
                                state = 0;
                            }
                            break;
                    }
                }
                imd = new IMEData();
                imd.key = "MyInput.";
                imd.Values = new ArrayList();
                imd.Values.Add("ခေါ်​သ​လား​ခင်​ဗျာ​။");
                al.Add(imd);
            }
        }

        public IMEData getData(string key)
        {
            foreach (IMEData i in al)
            {
                if (i.key.ToUpper() == key)
                    return i;
            }
            return null;
        }

        public IMEData MatchData(string bkey)
        {
            ArrayList alx = new ArrayList();
            foreach (IMEData i in al)
            {
                if (bkey.EndsWith(i.key.ToUpper()))
                    alx.Add(i);
            }
            if (alx.Count > 0)
            {
                int x = 0;
                int length = ((IMEData)alx[0]).key.Length;
                for (int j = 0; j < alx.Count; j++)
                {
                    if (((IMEData)alx[j]).key.Length > length)
                    {
                        length = ((IMEData)alx[j]).key.Length;
                        x = j;
                    }
                }
                return (IMEData)alx[x];
            }
            return null;
        }

        public ArrayList al;
        private string name;

        public string getname()
        {
            return name;
        }
    }


    public class IMEData
    {
        public string key;
        public ArrayList Values;
    }

}
