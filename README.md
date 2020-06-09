Project name:

Cocktail Magician
Team: N-9, .Net Alpha18, Telerik Academy

Yavor Vasilev - GitHub
Nikola Yanev - GitHub
Description:

Cocktail Magician is a web application for exploring recipes for exotic cocktails

and follows their distribution and success in amazing bars. The application

enables the users to see bars and the cocktails(with ingredients) they offer, rate

them and leave a comment for them.

Project Logo:

 

Access levels:

Public part - Visible for all website visitors - no authentication required.
Private part (Bar Crawlers) – with more access from public part
Administration Part (Cocktail Magicians) – website administrators with full access
Public part:

The public part consist of a home page displaying top rated bars and cocktails in separate sections on the page.
It also includes searching possibility for:
Bars: by Name, Adress, and Rating
Cocktails: by Name, Ingredient and Rating
Upon clicking a bar, the visitor can see details for the bar (image, name, rating, address, phone, and comments) and the cocktails it offers (with links to the cocktail).
Upon clicking a cocktail, the visitor can see details for the cocktail (image, name, rating, ingredients, and comments) and the bars this cocktail is offered in (with links to the bar).
The public part includes login page and register page as well.
Private part (Bar Crawlers):

After login, users see everything visible to website visitors and additionally they can:-

Rate bars (from 1 to 5 scale/stars)

Rate cocktails (regardless of which bar offers them) (from 1 to 5 scale/stars)

Leave a comment for a bar (maximum 500 characters)

Leave a comment for a cocktail (maximum 500 characters)

Administration Part (Cocktail Magicians):

Manage ingredients – CRUD operations for ingredients for cocktails (delete ingredient only if not used in any cocktail)
Manage cocktails – CRUD operations for cocktails (never delete a cocktail, just hide it from the users)
Manage bars – CRUD operations for bars (never delete a bar, just hide it from the users)
Set cocktails as available in particular bars
Technologies:

ASP.NET Core
ASP.NET Identity
EntityFramework Core
MS SQL Server
MS Test
HTML
CSS
Bootstrap
CloudScribe - for pegination
Git
