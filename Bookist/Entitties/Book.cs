using Bookist.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookist.Entities;

public class Book : BookBase
{
    public int Id { get; set; }
}