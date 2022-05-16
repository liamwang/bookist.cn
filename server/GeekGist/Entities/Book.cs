using System.ComponentModel.DataAnnotations;
using Anet.Entity;

namespace GeekGist.Entities;

public class Book : AuditEntity
{
    /// <summary>����</summary>
    [Required, Varchar(127)]
    public string Title { get; set; }

    /// <summary>����</summary>
    [Required, Varchar(127)]
    public string Subtitle { get; set; }

    /// <summary>���棨�ļ�Key��</summary>
    [Required, Varchar(255)]
    public string Cover { get; set; }

    /// <summary>����</summary>
    [Varchar(100)]
    public string Author { get; set; }

    /// <summary>��������</summary>
    [Date]
    public DateTime PubDate { get; set; }

    /// <summary>����</summary>
    [Required, Text]
    public string Intro { get; set; }

    /// <summary>�ٷ�����</summary>
    [Varchar(127)]
    public string OrgUrl { get; set; }

    /// <summary>�ļ���ʽ���á�/���ָ������</summary>
    [Required, Varchar(50)]
    public string Formats { get; set; }

    /// <summary>��ȡ����</summary>
    [Required, Varchar(127)]
    public string FetchUrl { get; set; }

    /// <summary>��ȡ��</summary>
    [Varchar(10)]
    public string FetchCode { get; set; }

    /// <summary>�Ķ���</summary>
    public int Views { get; set; }

    /// <summary>������</summary>
    public int Downloads { get; set; }

    public IList<Tag> Tags { get; set; } = new List<Tag>();
}