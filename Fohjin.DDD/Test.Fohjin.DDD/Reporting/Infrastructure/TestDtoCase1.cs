using System;
using System.Collections.Generic;

namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    public class TestDtoCase1
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
    public class TestDtoCase2
    {
        public int Column1 { get; set; }
        public double Column2 { get; set; }
        public float Column3 { get; set; }
    }
    public class TestDtoCase3
    {
        public Guid Id { get; set; }
        public Guid Column1 { get; set; }
    }
    public class TestDtoCase4
    {
        public string Column1 { get; set; }
        public List<string> Column2 { get; set; }
        public string Column3 { get; set; }
        public IEnumerable<string> Column4 { get; set; }
    }
}