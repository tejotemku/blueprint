import store from '@/store'
import { generateElementHtml } from './prototypeElementsHandlers.js'

export const generatePrototype = function() {
  const projectData = store.getters.getProjectData;
  let screens = projectData.screens;

  let generatedHtml = `
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width,initial-scale=1.0">
    <title>${projectData.title}</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.2/css/all.css" integrity="sha384-hWVjflwFxL6sNzntih27bfxkr27PmbbK/iSvJ+a4+0owXq79v+lsFkW54bOGbiDQ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <style>
      body {
        background-color: #5580EC;
        margin: 0 auto;
      }
      #container {
        height: ${projectData.height}px;
        width: ${projectData.width}px;
        background-color: #fff;
        margin: 0 auto;
        margin-top: 30px;
        overflow: hidden;
        position: relative;  
        outline: solid #c0c5ca 3px;
      }

      .elementWrapper {
        position: absolute;
        margin: 0 auto;
        height: ${projectData.height}px;
        width: ${projectData.width}px;
        overflow: hidden;
      }

      .rectangle {
        width: 80px;
        height: 50px;
        font-size: 0.8rem;
        text-align: center;
        text-overflow: ellipsis;
      }
      .rectangle-full {
        background-color: #000;
      }
      .rectangle-hollow {
        border: #000 1px solid;
      }
      .rectangle-full-rounded {
        background-color: #000;
        border-radius: 10%;
      }
      .rectangle-hollow-rounded {
        border: #000 1px solid;
        border-radius: 10%;
      }
      .circle {
        width: 80px;
        height: 80px;
        font-size: 0.8rem;
        text-align: center;
        text-overflow: ellipsis;
      }
      .circle-full {
        border-radius: 50%;
        background: #000;
      }
      .circle-hollow {
        border-radius: 50%;
        border: #000 1px solid;
      }
      .header-big {
        font-size: larger;
      }
      .header-xx-large {
        font-size: xx-large;
      }
      .header-small {
        font-size: small;
      }
      .header-tiny {
        font-size: xx-small;
      }
    </style>
  </head>
  <body>
    <noscript>
      <strong>We're sorry but prototype doesn't work properly without JavaScript enabled. Please enable it to continue.</strong>
    </noscript>
    <div id="container">
    </div>
    <script>
      let screens = ${parseScenes()};
      let currScreenId = "${projectData.defaultScreenId}";
      changeDisplayedScreen();

      function changeDisplayedScreenId(newScreenId) {
        currScreenId = newScreenId;
        changeDisplayedScreen();
      }
      function changeDisplayedScreen() {
        let container = document.getElementById("container");
        container.innerHTML  = screens[currScreenId];
      }
    </script>
  </body>
</html>
`  
  // Start file download.
  download(projectData.title + ".html", generatedHtml);

  function download(filename, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
  }

  function parseScenes() {
    let parsedScreens = "{";
    for (const [screenId, screenData] of Object.entries(screens)) {
      parsedScreens += `'${screenId}': \``;
      screenData.elements.forEach(function(element, index) {
        let elementHtml = generateElementHtml(element);
        elementHtml = elementHtml.replace(/[\n\r]/g, ' ');
        let parsedScreenElement = `
          <div
            ${element.properties.redirect? `onclick="changeDisplayedScreenId('${element.properties.redirect}')"` : ""} 
            style="position: absolute; top: ${parseInt(element.top)}px; left: ${parseInt(element.left)}px;z-index: ${99999 - index};"
          >
            ${elementHtml}
          </div>
        `;
        parsedScreenElement = parsedScreenElement.replace(/[\n\r]/g, ' ');
        parsedScreens += parsedScreenElement;
      })
      parsedScreens += `\`,`;
    }
    parsedScreens += `}`;
    return parsedScreens;
  }

}

