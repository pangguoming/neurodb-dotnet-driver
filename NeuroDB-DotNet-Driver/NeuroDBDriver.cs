using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NeuroDB_DotNet_Driver
{
    public class NeuroDBDriver
    {

        const int NEURODB_RETURNDATA = 1;
        const int NEURODB_SELECTDB = 2;
        const int NEURODB_EOF = 3;
        const int NEURODB_NODES = 6;
        const int NEURODB_LINKS = 7;
        const int NEURODB_EXIST = 17;
        const int NEURODB_NIL = 18;
        const int NEURODB_RECORD = 19;
        const int NEURODB_RECORDS = 20;

        const int NDB_6BITLEN = 0;
        const int NDB_14BITLEN = 1;
        const int NDB_32BITLEN = 2;
        const int NDB_ENCVAL = 3;
        //const   int NDB_LENERR =UINT_MAX;

        const int VO_STRING = 1;
        const int VO_NUM = 2;
        const int VO_STRING_ARRY = 3;
        const int VO_NUM_ARRY = 4;
        const int VO_NODE = 5;
        const int VO_LINK = 6;
        const int VO_PATH = 7;
        const int VO_VAR = 8;
        const int VO_VAR_PATTERN = 9;

        class StringCur
        {
            byte[] s;
            int cur;

            public StringCur(byte[] body)
            {
                this.s = body;
                this.cur = 0;
            }

            public String get(int size)
            {
                byte[] bytes = new byte[size];
                Array.Copy(this.s, this.cur, bytes, 0, size);
                String subStr = System.Text.Encoding.UTF8.GetString(bytes);
                cur += size;
                return subStr;
            }

            public char getType()
            {
                char type = (char)this.s[this.cur];
                cur += 1;
                return type;
            }
        }


        private Socket s;

        public NeuroDBDriver(String ip, int port)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(ip);      //获取设置的IP地址
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, port);    //指定的端口号和服务器的ip建立一个IPEndPoint对象
            s.Connect(iPEndPoint);

        }

        public void close()
        {
            s.Close();
        }

        public ResultSet executeQuery(String query)
        {

            byte[] byteArray = System.Text.Encoding.Default.GetBytes(query);
            this.s.Send(byteArray);

            ResultSet resultSet = new ResultSet();
            byte[] receive = new byte[1];
            int len=this.s.Receive(receive);
            if(len==0) throw new Exception("network error");
            char type = (char)receive[0];
            switch (type)
            {
                case '@':
                    resultSet.setStatus(((int)ResultStatus.PARSER_OK));
                    break;
                case '$':
                    resultSet.setMsg(readLine(this.s));
                    break;
                case '#':
                    resultSet.setMsg(readLine(this.s));
                    break;
                case '*':
                    String line = readLine(this.s);
                    String[] head = line.Split(',');
                    resultSet.setStatus(int.Parse(head[0]));
                    resultSet.setCursor(int.Parse(head[1]));
                    resultSet.setResults(int.Parse(head[2]));
                    resultSet.setAddNodes(int.Parse(head[3]));
                    resultSet.setAddLinks(int.Parse(head[4]));
                    resultSet.setModifyNodes(int.Parse(head[5]));
                    resultSet.setModifyLinks(int.Parse(head[6]));
                    resultSet.setDeleteNodes(int.Parse(head[7]));
                    resultSet.setDeleteLinks(int.Parse(head[8]));

                    int bodyLen = int.Parse(head[9]);
                    byte[] body = new byte[bodyLen];
                    len = this.s.Receive(body);
                    readLine(this.s);
                    RecordSet recordSet = deserializeReturnData(body);
                    resultSet.setRecordSet(recordSet);
                    break;
                default:
                    throw new Exception("reply type error");
            }
            return resultSet;

        }

        static char deserializeType(StringCur cur)
        {
            return cur.getType();
        }

        static int deserializeUint(StringCur cur)
        {
            int[] buf = new int[3];
            buf[0] = cur.get(1).ToCharArray()[0];
            buf[1] = cur.get(1).ToCharArray()[0];
            buf[2] = cur.get(1).ToCharArray()[0];
            return (buf[0] & 0x7f) << 14 | (buf[1] & 0x7f) << 7 | buf[2];
        }

        static String deserializeString(StringCur cur)
        {
            int len = deserializeUint(cur);
            String val = cur.get(len);
            return val;
        }

        static List<String> deserializeStringList(StringCur cur)
        {
            int listlen = deserializeUint(cur);
            List<String> l = new List<String>();
            while (listlen-- > 0)
            {
                String s = deserializeString(cur);
                l.Add(s);
            }
            return l;
        }

        static List<String> deserializeLabels(StringCur cur, List<String> labeList)
        {
            int listlen = deserializeUint(cur);
            List<String> l = new List<String>();
            while (listlen-- > 0)
            {
                int i = deserializeUint(cur);
                l.Add(labeList[i]);
            }
            return l;
        }

        static Hashtable deserializeKVList(StringCur cur, List<String> keyNames)
        {
            int listlen = deserializeUint(cur);
            Hashtable properties = new Hashtable();
            while (listlen-- > 0)
            {
                int i = deserializeUint(cur);
                String key = keyNames[i];
                int type;
                type = deserializeUint(cur);
                int aryLen = 0;
                ColVal val = new ColVal();
                val.setType(type);
                if (type == VO_STRING)
                {
                    val.setVal(deserializeString(cur));
                }
                else if (type == VO_NUM)
                {
                    String doubleStr = deserializeString(cur);
                    val.setVal(Double.Parse(doubleStr));
                }
                else if (type == VO_STRING_ARRY)
                {
                    aryLen = deserializeUint(cur);
                    String[] valAry = new String[aryLen];
                    for (i = 0; i < aryLen; i++)
                    {
                        valAry[i] = deserializeString(cur);
                    }
                    val.setVal(valAry);
                }
                else if (type == VO_NUM_ARRY)
                {
                    aryLen = deserializeUint(cur);
                    double[] valAry = new double[aryLen];
                    for (i = 0; i < aryLen; i++)
                    {
                        String doubleStr = deserializeString(cur);
                        valAry[i] = Double.Parse(doubleStr);
                    }
                    val.setVal(valAry);
                }
                else
                {
                    //printf("loading pkvType ERROR ");
                    throw new Exception("Error Type");
                }
                properties.Add(key, val);
            }
            return properties;
        }

        static Node deserializeCNode(StringCur cur, List<String> labels, List<String> keyNames)
        {
            long id;
            List<String> nlabels = null;
            Hashtable kvs = null;
            id = deserializeUint(cur);
            nlabels = deserializeLabels(cur, labels);
            kvs = deserializeKVList(cur, keyNames);
            Node n = new Node(id, nlabels, kvs);
            return n;
        }

        static Link deserializeCLink(StringCur cur, List<String> types, List<String> keyNames)
        {
            long id, hid, tid;
            int typeIndex;
            String type = null;
            Hashtable kvs = null;
            id = deserializeUint(cur);
            hid = deserializeUint(cur);
            tid = deserializeUint(cur);
            int ty;
            ty = deserializeType(cur);
            if (ty == NEURODB_EXIST)
            {
                typeIndex = deserializeUint(cur);
                type = types[typeIndex];
            }
            else if (ty == NEURODB_NIL)
            {
            }
            kvs = deserializeKVList(cur, keyNames);
            Link l = new Link(id, hid, tid, type, kvs);
            return l;
        }

        Node getNodeById(List<Node> nodes, long id)
        {
            foreach (Node node in nodes)
            {
                if (node.getId() == id)
                    return node;
            }
            return null;
        }

        Link getLinkById(List<Link> links, long id)
        {
            foreach (Link link in links)
            {
                if (link.getId() == id)
                    return link;
            }
            return null;
        }

        RecordSet deserializeReturnData(byte[] body)
        {
            StringCur cur = new StringCur(body);
            RecordSet rd = new RecordSet();
            List<object> path = null;
            /*读取labels、types、keyNames列表*/
            if (deserializeType(cur) != NEURODB_RETURNDATA)
                throw new Exception("Error Type");
            rd.setLabels(deserializeStringList(cur));
            rd.setTypes(deserializeStringList(cur));
            rd.setKeyNames(deserializeStringList(cur));
            /*读取节点列表*/
            if (deserializeType(cur) != NEURODB_NODES)
                throw new Exception("Error Type");
            int cnt_nodes;
            cnt_nodes = deserializeUint(cur);
            for (int i = 0; i < cnt_nodes; i++)
            {
                Node n = deserializeCNode(cur, rd.getLabels(), rd.getKeyNames());
                rd.getNodes().Add(n);
            }
            /*读取关系列表*/
            if (deserializeType(cur) != NEURODB_LINKS)
                throw new Exception("Error Type");
            int cnt_links;
            cnt_links = deserializeUint(cur);
            for (int i = 0; i < cnt_links; i++)
            {
                Link l = deserializeCLink(cur, rd.getTypes(), rd.getKeyNames());
                rd.getLinks().Add(l);
            }
            /*读取return结果集列表*/
            if (deserializeType(cur) != NEURODB_RECORDS)
                throw new Exception("Error Type");
            int cnt_records;
            cnt_records = deserializeUint(cur);
            for (int i = 0; i < cnt_records; i++)
            {
                int type, cnt_column;
                if (deserializeType(cur) != NEURODB_RECORD)
                    throw new Exception("Error Type");
                cnt_column = deserializeUint(cur);
                List<ColVal> record = new List<ColVal>();
                for (int j = 0; j < cnt_column; j++)
                {
                    int aryLen = 0;
                    type = deserializeType(cur);
                    ColVal val = new ColVal();
                    val.setType(type);
                    if (type == NEURODB_NIL)
                    {
                        /*val =NULL;*/
                    }
                    else if (type == VO_NODE)
                    {
                        int id;
                        id = deserializeUint(cur);
                        Node n = getNodeById(rd.getNodes(), id);
                        val.setVal(n);
                    }
                    else if (type == VO_LINK)
                    {
                        int id;
                        id = deserializeUint(cur);
                        Link l = getLinkById(rd.getLinks(), id);
                        val.setVal(l);
                    }
                    else if (type == VO_PATH)
                    {
                        int len;
                        len = deserializeUint(cur);
                        path = new List<object>();
                        for (i = 0; i < len; i++)
                        {
                            int id;
                            id = deserializeUint(cur);
                            if (i % 2 == 0)
                            {
                                Node nd = getNodeById(rd.getNodes(), id);
                                path.Add(nd);
                            }
                            else
                            {
                                Link lk = getLinkById(rd.getLinks(), id);
                                path.Add(lk);
                            }
                        }
                        val.setVal(path);
                    }
                    else if (type == VO_STRING)
                    {
                        val.setVal(deserializeString(cur));
                    }
                    else if (type == VO_NUM)
                    {
                        String doubleStr = deserializeString(cur);
                        val.setVal(Double.Parse(doubleStr));
                    }
                    else if (type == VO_STRING_ARRY)
                    {
                        aryLen = deserializeUint(cur);
                        String[] valAry = new String[aryLen];
                        for (i = 0; i < aryLen; i++)
                        {
                            valAry[i] = deserializeString(cur);
                        }
                        val.setVal(valAry);
                    }
                    else if (type == VO_NUM_ARRY)
                    {
                        aryLen = deserializeUint(cur);
                        double[] valAry = new double[aryLen];
                        for (i = 0; i < aryLen; i++)
                        {
                            String doubleStr = deserializeString(cur);
                            valAry[i] = Double.Parse(doubleStr);
                        }
                        val.setVal(valAry);
                    }
                    else
                    {
                        throw new Exception("Error Type");
                    }
                    record.Add(val);
                }
                rd.getRecords().Add(record);
            }
            /*读取结束标志*/
            if (deserializeType(cur) != NEURODB_EOF)
                throw new Exception("Error Type");
            return rd;
        }

        static String readLine(Socket socket)
        {
            StringBuilder sb = new StringBuilder();
            byte[] bytes = new byte[1]; // 一次读取一个byte
            while (socket.Receive(bytes) != -1)
            {
                char c = (char)bytes[0];
                sb.Append(c);
                if (c == '\n')
                {
                    break;
                }
            }
            return sb.ToString().Replace("\r\n", "");
        }
    }
}
