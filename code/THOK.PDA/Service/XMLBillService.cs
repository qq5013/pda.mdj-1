using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using THOK.PDA.Util;

namespace THOK.PDA.Service
{
    public class XMLBillService
    {
        XmlDocument doc = new XmlDocument();
        string uploadFile = "uploadFile.xml";
        string importFile = "exportBill.xml";

        public XMLBillService()
        {

        }
        public void SaveBill(string billId, string detailId)
        {
            XmlElement root = null;
            if (File.Exists(uploadFile))
            {
                doc.Load(uploadFile);
                root = doc.DocumentElement;
            }
            else
            {
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "GB2312", ""));
                root = doc.CreateElement("Bill");
                doc.AppendChild(root);
            }

            XmlElement node = doc.CreateElement("billInfo");
            node.SetAttribute("masteId", billId);
            node.SetAttribute("detailId", detailId);
            root.AppendChild(node);
            doc.Save(uploadFile);
        }
        public void ReadBill()
        {
            if (!File.Exists(importFile))
            {
                SystemCache.DetailTable = null;
                SystemCache.MasterTable = null;
                return;
            }
            DataTable detailTable = null;
            DataTable masterTable = null;

            doc.Load(importFile);
            XmlElement root = doc.DocumentElement;

            if (root.ChildNodes.Count == 0)
            {
                SystemCache.DetailTable = null;
                SystemCache.MasterTable = null;
                return;
            }
            foreach (XmlElement node in root.ChildNodes)
            {
                //ΪDataTable������ṹ
                if (masterTable == null)
                {
                    masterTable = new DataTable();
                    foreach (XmlAttribute masterAttribute in node.Attributes)
                    {
                        masterTable.Columns.Add(masterAttribute.Name);
                    }
                }
                if (detailTable == null)
                {
                    if (node.ChildNodes.Count > 0)
                    {
                        detailTable = new DataTable();
                        foreach (XmlAttribute detailAttribute in node.ChildNodes[0].Attributes)
                        {
                            detailTable.Columns.Add(detailAttribute.Name);
                        }
                    }
                }
                //ΪDataTable������� �������������
                DataRow masterRow = masterTable.NewRow();
                foreach (XmlAttribute masterAttribute in node.Attributes)
                {
                    masterRow[masterAttribute.Name] = masterAttribute.Value;
                }
                masterTable.Rows.Add(masterRow);
                //�����ϸ��������
                foreach (XmlElement detailNode in node.ChildNodes)
                {
                    DataRow detailRow = detailTable.NewRow();
                    foreach (XmlAttribute detailAttribute in detailNode.Attributes)
                    {
                        detailRow[detailAttribute.Name] = detailAttribute.Value;
                    }
                    detailTable.Rows.Add(detailRow);
                }
            }
            SystemCache.MasterTable = masterTable;
            SystemCache.DetailTable = detailTable;
        }
        public void UpdateBill(string billId, string detailId, string piece, string item)
        {
            doc.Load(importFile);
            XmlNode node = doc.SelectNodes(@"/Bill/billInfo/detailInfo[@MASTER='" + billId + "'][@DETAILID='" + detailId + "']").Item(0);
            node.Attributes["CONFIRMSTATE"].Value = "3";

            node.Attributes["STATENAME"].Value = "��ִ��";
            node.Attributes["OPERATEPIECE"].Value = piece;
            node.Attributes["OPERATEITEM"].Value = item;
            doc.Save(importFile);
            SystemCache.DetailTable.Select("MASTER='" + billId + "' AND DETAILID='" + detailId + "'")[0]["CONFIRMSTATE"] = "3";
        }
    }
}
