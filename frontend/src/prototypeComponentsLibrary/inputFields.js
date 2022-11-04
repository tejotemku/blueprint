export const inputFieldsComponents = function() {
  return [
    {
      previewHtml: '<input disabled type="text" class="form-control" placeholder="Text Input Field"/>',
      class: 'form-control',
      inputType: 'text',
      textColor: 'black',
      text: 'text field',
      defaultHeight: 50,
      defaultWidth: 200,
      description: "Text Input Field",
    },
    {
      previewHtml: '<div style="display: flex;" dragable> <input disabled class="mr-1" type="checkbox" checked /> Checkbox </div>',
      class: 'form-check-input',
      inputType: 'checkbox',
      textColor: 'black',
      defaultHeight: 20,
      defaultWidth: 20,
      isChecked: true,
      description: "Checkbox",
    },
    {
      previewHtml: '<input disabled type="text" class="form-control" placeholder="Password" />',
      class: 'form-control',
      inputType: 'password',
      textColor: 'black',
      text: 'password',
      defaultHeight: 50,
      defaultWidth: 200,
      description: "Password Field",
    },
    {
      previewHtml: '<div style="display: flex;" dragable> <input disabled class="form-control mr-1" type="color"/> Color picker </div>',
      class: 'form-control',
      inputType: 'color',
      textColor: 'black',
      defaultHeight: 50,
      defaultWidth: 62,
      description: "Color Picker",
    },
    {
      previewHtml: '<div dragable><input disabled class="form-control" type="file"/></div>',
      class: 'form-control',
      inputType: 'file',
      textColor: 'black',
      defaultHeight: 50,
      defaultWidth: 200,
      description: "File Picker",
    },
    {
      previewHtml: '<div dragable><input disabled type="range" class="form-range"/></div>',
      class: 'form-control',
      inputType: 'range',
      textColor: 'black',
      defaultHeight: 50,
      defaultWidth: 200,
      description: "Range picker",
    },
  ];
}