export const generateElementHtml = (element) => {
  const handlers = {
    'Image': handleImage,
    'Button': handleButton,
    'InputField': handleInputField
  }
  let generatedHtml = '-=-';
  const positionStyle = `
    ${element['properties']['height']? 'height: ' + element['properties']['height'] + 'px;': ''} 
    ${element['properties']['width']? 'width: ' + element['properties']['width'] + 'px;': ''} 
  `;
  try {
     handlers[element.type]();
  }
  catch {
    console.log('Unknown Element');
  }
  return generatedHtml;

  function handleImage() {
    const style = `
      ${element['properties']['height'] || element['properties']['width']? 'object-fit: cover;': ''} 
    `;
    generatedHtml  = `<img src="${element['properties']['src']}" style="${positionStyle + style}" />`;
  }

  function handleButton() {
    const style = `
      ${element['properties']['backgroundColor']? 'background-color: ' + element['properties']['backgroundColor'] + ';': ''} 
      ${element['properties']['textColor']? 'color: ' + element['properties']['textColor'] + ';' : ''} 
      
    `;
    generatedHtml  = `<button style="${positionStyle + style}">${element['properties']['text']}</button>`;
  }

  function handleInputField() {
    const style = `
      ${element['properties']['backgroundColor']? 'background-color: ' + element['properties']['backgroundColor'] + ';' : ''} 
      ${element['properties']['textColor']? 'color: ' + element['properties']['textColor'] + ';' : ''} 
    `;
    generatedHtml  = `<input type="${element['properties']['inputType']}" style="${positionStyle + style}" placeholder="${element['properties']['placeholder']}" />`;
  }
}