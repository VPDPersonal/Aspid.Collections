# Website

The Docusaurus theme follows Aspid.FastTools. Keep English and Russian guides aligned.
Runtime source code is the authority for API behavior. Generated API pages are committed.

- `npm ci`, `npm run sync-content`, `npm run check-content`, `npm test`, then `npm run build`.
- After changing public runtime API, run `npm run api` with .NET 10 installed.
- Package sample READMEs remain the authority for Russian tutorials and previews; `npm run sync-content` copies them.
- Do not overwrite the repository READMEs from the website. The changelog is copied from the repository on build.
- Ignore `node_modules`, `build`, generated changelog pages, and imported preview copies.
