export const generateElementHtml = (element, disableComponent=false) => {
  const handlers = {
    "Image": handleImage,
    "Button": handleButton,
    "InputField": handleInputField,
    "Shape": handleShape
  }
  let generatedHtml = "";
  const positionStyle = `
    ${element.properties.height? "height: " + element.properties.height + "px;": ""} 
    ${element.properties.width? "width: " + element.properties.width + "px;": ""}
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
        style="${positionStyle + style}" 
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
        style="${positionStyle + style}" 
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
        style="${positionStyle + style}" 
        placeholder="${element.properties.text}" 
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
        style="${positionStyle + style}" 
        ${element.properties.class? 
        "class=\"" + element.properties.class + "\"" :""} 
      />`;
  }
}