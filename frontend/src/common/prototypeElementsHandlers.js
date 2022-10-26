export const generateElementHtml = (element, disableComponent=false) => {
  const handlers = {
    "Image": handleImage,
    "Button": handleButton,
    "InputField": handleInputField,
    "Shape": handleShape,
    "TextField": handleTextField
  }
  let generatedHtml = "";
  const generalStyle = `
    ${element.properties.height? "height: " + element.properties.height + "px;": ""} 
    ${element.properties.width? "width: " + element.properties.width + "px;": ""}
    word-break: break-all;
  `;
  try {
     handlers[element.type]();
  }
  catch {
    console.log("Unknown Element");
  }
  return generatedHtml;

  function handleImage() {
    // General Properties
    // src - url to image
    // imageLimitMode - should image be cropped od scaled down or what
    const imageLimitModes = {
      "scale": "contain",
      "crop": "cover"
    }
    const style = `
      ${element.properties.height || element.properties.width? "object-fit:" + imageLimitModes[element.properties.imageLimitMode] + ";": ""}
    `;
    generatedHtml  = `
      <img 
        src="${element.properties.src}" 
        style="${generalStyle + style}" 
      />`;
  }

  function handleButton() {
      // General Properties
      // text - text displayed on button
      // textColor - color of displayed text
      // backgroundColor - background color of button
      // class - preset class of a button

    const style = `
      ${element.properties.backgroundColor? "background-color: " + element.properties.backgroundColor + ";": ""} 
      ${element.properties.textColor? "color: " + element.properties.textColor + ";" : ""}
    `;
    generatedHtml  = `
      <button 
        style="${generalStyle + style}" 
        ${element.properties.class? "class=\"" + element.properties.class + "\"" :""} 
      >
          ${element.properties.text}
      </button>`;
  }

  function handleInputField() {
      // General Properties
      // placeholder - text displayed on placeholder
      // textColor - color of displayed text
      // backgroundColor - background color of input field
      // class - preset class of a input field
      // inputType - type of html input field
    const style = `
      ${element.properties.backgroundColor? "background-color: " + element.properties.backgroundColor + ";" : ""} 
      ${element.properties.textColor? "color: " + element.properties.textColor + ";" : ""}
    `;
    generatedHtml  = `
      <input 
        ${disableComponent ? "diabled" : ""} 
        type="${element.properties.inputType}" 
        ${element.properties.class? "class=\"" + element.properties.class + "\"" :""} 
        style="${generalStyle + style}" 
        placeholder="${element.properties.text}" 
        ${element.properties.checked? "checked": ""}
      />`;
  }

  function handleShape() {
    // General Properties
    // shape - shape html
    // backgroundColor - background color of shape

    
    const style = `
      ${element.properties.backgroundColor? "background-color: " + element.properties.backgroundColor + ";" : ""}
    `;
    generatedHtml  = `
      <div 
        style="${generalStyle + style}" 
        ${element.properties.class? 
        "class=\"" + element.properties.class + "\"" :""} 
      />`;
  }

  function handleTextField() {
    // General Properties
    // textColor - text color of text field

    
    const style = `
      ${element.properties.textColor? "color: " + element.properties.textColor + ";" : ""}
      word-break: normal;
    `;
    generatedHtml  = `
      <div 
        style="${generalStyle + style}" 
      >
      ${element.properties.text}
      </div>`;
  }
}