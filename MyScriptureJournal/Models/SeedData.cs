using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;

namespace MyScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                if (context == null || context.Entries == null)
                {
                    throw new ArgumentNullException("Null MyScriptureJournalContext");
                }

                // Look for any journal entries
                if (context.Entries.Any())
                {
                    return; // DB has been seeded
                }

                context.Entries.AddRange(
                    new Entry
                    {
                        Book = "1 Nephi",
                        Chapter = 20,
                        Verses = "1-5",
                        DateAdded = DateTime.Parse("2021-5-12"),
                        JournalEntry = "I love these verses. They remind me of my mom."
                    },

                    new Entry
                    {
                        Book = "3 Nephi",
                        Chapter = 2,
                        Verses = "10",
                        DateAdded = DateTime.Parse("2023-1-10"),
                        JournalEntry = "I am so grateful for forgiveness."
                    },

                    new Entry
                    {
                        Book = "Moroni",
                        Chapter = 5,
                        Verses = "16-17",
                        DateAdded = DateTime.Parse("2022-9-1"),
                        JournalEntry = "Such a powerful statement."
                    },

                    new Entry
                    {
                        Book = "Omni",
                        Chapter = 1,
                        Verses = "1-5",
                        DateAdded = DateTime.Parse("2000-12-5"),
                        JournalEntry = "Short. Sweet. Insightful."
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
