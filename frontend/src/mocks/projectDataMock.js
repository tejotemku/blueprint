export const projectData = () => {
  return {
    title: "Mock Project Name",
    width: 1920,
    height: 1080,
    screens: {
      '1231sda3q2': {
        name: "Mock Screen 1",
        elements: [
          {
            id: '0',
            type: 'Button',
            top: 60,
            left: 50,
            description: "Button - 1",
            properties: {
              redirect: "fg43r23r",
              text: 'Button - 1',
              backgroundColor: 'red',
              class: 'btn btn-primary',
              width: null,
              height: null,
            }
          },
          {
            id: '2',
            type: 'InputField',
            top: 60,
            left: 300,
            description: 'Email Field - 1',
            properties: {
              class:"form-control",
              redirect: null,
              inputType: 'email',
              text: 'e.g. gerwant@gmail.com',
              backgroundColor: 'cyan',
              textColor: '#fff',
              width: '250',
              height: null,
            }
          },
          {
            id: '3',
            type: 'Image',
            top: 160,
            left: 300,
            description: 'Image - 1',
            properties: {
              redirect: null,
              src: 'https://i.stack.imgur.com/sjpNp.png',
              width: '256',
              height: '128',
              imageLimitMode: 'crop',
              backgroundColor: null,
            }
          },
        ]
      },
      'fg43r23r': {
        name: "Mock Screen 2",
        elements: [
          {
            id: '1',
            type: 'Button',
            top: 100,
            left: 40,
            description: 'Button - 2',
            properties: {
              redirect: "1231sda3q2",
              text: 'Button - 2',
              backgroundColor: '#f9c',
              textColor: 'white',
              width: null,
              height: null,
            }
          },
        ]
      }
    },
    assets: [
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic1.png',
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic3.png',
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic4.png',
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic5.png',
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic6.png',
      'https://raw.githubusercontent.com/Stowarzyszenie-Umarlych-Kotletow/nerdchat/master/my-app/public/assets/NerdchatDefPic7.png',
    ],
    currentScreenId: '1231sda3q2',
    defaultScreenId: '1231sda3q2',
  }
}