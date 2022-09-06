export const handleElement = (element) => {
  const handlers = {
    'Image': handleImage
  }
  try {
    handlers[element.type]();
  }
  catch {
    console.log('Unknown Element');
  }
  return element;

  function handleImage() {
    element['html'] = `<img src="${element['properties']['src']}"/>`
  }
}