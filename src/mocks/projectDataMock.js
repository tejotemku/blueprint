export const projectData = () => {
  return {
    title: "Mock Project Name",
    width: 1920,
    height: 1080,
    screens: {
      '0': {
        id: '0',
        name: "Mock Screen 1",
        elements: [
          {
            id: 0,
            type: 'Button',
            top: 60,
            left: 50,
            description: "Button - 1",
            properties: {
              text: 'Button - 1',
              backgroundColor: 'red'
            }
          },
          {
            id: 2,
            type: 'InputField',
            top: 60,
            left: 300,
            description: 'Email Field - 1',
            properties: {
              inputType: 'email',
              placeholder: 'e.g. gerwant@gmail.com',
              backgroundColor: 'cyan',
              textColor: '#fff'
            }
          },
          {
            id: 3,
            type: 'Image',
            top: 160,
            left: 300,
            description: 'Image - 1',
            properties: {
              src: 'https://i.stack.imgur.com/sjpNp.png',
              width: '256',
              height: '128',
            }
          },
        ]
      },
      '1': {
        id: '1',
        name: "Mock Screen 2",
        elements: [
          {
            id: 1,
            type: 'Button',
            top: 100,
            left: 40,
            description: 'Button - 2',
            properties: {
              text: 'Button - 2',
              backgroundColor: '#f9c',
              textColor: 'white'
            }
          },
        ]
      }
    },
    assets: [
      'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/2560px-Google_2015_logo.svg.png',
      'https://i.pinimg.com/originals/ce/af/83/ceaf8384322af790486cff176a0a2f24.png',
      'https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Playstation_logo_colour.svg/1009px-Playstation_logo_colour.svg.png'  
    ],
    currentScreenId: '0'
  }
}