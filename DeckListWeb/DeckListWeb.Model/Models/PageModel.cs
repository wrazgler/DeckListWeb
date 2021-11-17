﻿using System;

namespace DeckListWeb.Model.Models
{
    public class PageModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }

        public PageModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => (PageNumber > 1);

        public bool HasNextPage => (PageNumber < TotalPages);
    }
}
