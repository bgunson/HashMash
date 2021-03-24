# HashMash

HashMash is an educational tool which with the intention of visually demonstrating hashing of plain text. The user clicks an incrementer and progresses through levels and unlocks new hashing techniques.

___

### Build Instructions
A workflow has been set up to continuosly publish the application to GitHub Pages for every push to _main_. Currently, the workflow is disabled, so pushes to main are not reflected on the _gh-pages_ branch (which the application is built from). So, when developing a few things need to be accounted for:

##### During Development
When testing/building the application with VS, be sure of the following in _wwwroot/index.html_
- Open _index.html_ and assert that the base url within the _head_ is "/" in your own environment/branch 

        <html>
          <head>
            ...
            <base href="/" />
            ...
          </head>
          ...

##### When Deploying to GitHub Pages
When a new version is to be pushed to the site, make sure the workflow under _Actions_ is enabled, then
- Open _wwwroot/index.html_ and assert that the base url within the _head_ is "/HashMash/" on the _main_ branch  

        <html>
          <head>
            ...
            <base href="/HashMash/" />
            ...
          </head>
          ...
          
 The workflow cannot successfully build if the base url is not "/HashMash" since the Pages site is within a sub repo.
 ___
