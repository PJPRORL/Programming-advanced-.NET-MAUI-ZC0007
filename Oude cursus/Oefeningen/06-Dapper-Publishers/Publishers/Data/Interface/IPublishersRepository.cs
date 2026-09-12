using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publishers.Data.Interface
{
    public interface IPublishersRepository
    {
        public IEnumerable<Publisher> OphalenPublishers();

        public IEnumerable<Publisher> OphalenPublishers(string zoekterm);
    }
}