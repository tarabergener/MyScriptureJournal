using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Entry> Entries { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookName { get; set; }
        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            // </snippet_search_linqQuery>
            IQueryable<string> bookQuery = from e in _context.Entries
                                           orderby e.Book
                                           select e.Book;
            // </snippet_search_linqQuery>

            var entries = from e in _context.Entries
                          select e;
            if (!string.IsNullOrEmpty(SearchString))
            {
                entries = entries.Where(s  => s.Book.Contains(SearchString) || s.JournalEntry.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(BookName))
            {
                entries = entries.Where(b => b.Book == BookName);
            }

            // <snippet_search_selectList>
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            // </snippet_search_selectList>

            switch (sortOrder)
            {
                case "book_desc":
                    entries = entries.OrderByDescending(e => e.Book);
                    break;
                case "Date":
                    entries = entries.OrderBy(e => e.DateAdded);
                    break;
                case "date_desc":
                    entries = entries.OrderByDescending(e => e.DateAdded);
                    break;
            }

            Entries = await entries.AsNoTracking().ToListAsync();
        }
    }
}
