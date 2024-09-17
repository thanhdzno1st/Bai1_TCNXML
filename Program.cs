using System;
using System.Xml;
using System.IO;
 
namespace Chapter2_ConsoleApplication1{   
 
   class Program {
       static string path1 = "D:/Workspace VScode/C#/Baitap1XML/bin/debug/data/students.xml";
// static string path1 = "./data/students.xml"; chú ý 
       static string path2 = "D:/Workspace VScode/C#/Baitap1XML/bin/debug/data/sinhvien.xml";
       static XmlDocument XmlDoc = new XmlDocument();
      static XmlElement node;
 
       static void Main(string[] args){
           List1(path1);
          
           update("S01", "Trach Van Doanh");
           Insert("S04", "Hoang My Le", "217XML01");           
           List2(path2);
 
           delete("S04");
           List2(path2);
           Console.ReadLine();
       }
 
       static XmlDocument OpenXml(String path)
{
    // Kiểm tra và load tệp           
    if (File.Exists(path))
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            return null; // Đảm bảo trả về null khi có lỗi
        }
    }
    else
    {
        Console.WriteLine(path + " not found");
        return null; // Đảm bảo trả về null khi tệp không tồn tại
    }
}

 
 
       static void List1(string path){
           // Hiển thị danh sách sinh viên trong students.xml
            // Mục đích: hiểu thẻ chứa và thuộc tính InnerText 
           XmlDoc= OpenXml(path);
           if (XmlDoc != null){
               XmlNodeList list = 
                          XmlDoc.GetElementsByTagName("student");
               Console.WriteLine("\n" +list.Count +
                           " sinh vien trong " + path);
 
               foreach (XmlElement e in list){
                   string hoten = e.ChildNodes[0].InnerText;
                   string lop = e.ChildNodes[1].InnerText;
                   Console.WriteLine(hoten + "-" + lop);
               }
           }
       }
 
       static void List2(string path){
           // Hiển thị danh sách sinh viên trong sinhvien.xml
           XmlDoc=OpenXml(path);
           if (XmlDoc != null){
               XmlNodeList list = 
                          XmlDoc.GetElementsByTagName("student");
               Console.WriteLine("\n"+list.Count + 
                           " sinh vien trong " + path);
 
               foreach (XmlElement e in list){
                   string hoten = e.GetAttribute("name");
                   string lop = e.GetAttribute("class");
                   Console.WriteLine(hoten + "-" + lop);
               }
           }
       }
 
       static void Insert(string code, string name, string group){           
           Console.WriteLine("\nInsert new node into sinhvien.xml");
           XmlDoc = OpenXml(path2);
           XmlElement stu=XmlDoc.CreateElement("student");
           stu.SetAttribute("code",code);
           stu.SetAttribute("name",name);
           stu.SetAttribute("class",group);
          XmlDoc.DocumentElement.AppendChild(stu);
           XmlDoc.Save(path2);
       }
 
       static XmlElement Search(String code){
           // Tìm kiếm sinh viên theo mã số trong sinhvien.xml
           XmlElement stu=null;
           XmlDoc = OpenXml(path2);
           XmlNodeList list = XmlDoc.GetElementsByTagName("student");
           foreach (XmlElement e in list){
               if (e.GetAttribute("code") == code){
                   stu = e;
                   break;
               }
           }
           return stu;
       }  // end function insert
 
       static void delete(string code){
           Console.WriteLine("Xoa node co ma so "+code);
           XmlDoc = OpenXml(path2);
           node = Search(code);
           if (node != null){
               XmlElement father = (XmlElement)node.ParentNode;
               father.RemoveChild(node);
               XmlDoc.Save(path2);
           }
       }  // end function delete
 
       static void update(string code,string newname){
           Console.WriteLine("Cap nhat node có ma so "+code);
           node = Search(code);
           if (node != null){
               node.SetAttribute("name",newname);
               XmlDoc.Save(path2);
               List2(path2);
           }
       }
   }      // end class
}           // end namespace