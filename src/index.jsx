import React from 'react';
import ReactDOM from 'react-dom';

function Button() {
  return <button id="btn">Say Hello</button>;
}

const elem = document.createElement('div');
document.body.appendChild(elem);

ReactDOM.render(
  <Button />,
  elem,
  function() {
    console.log('settings click event now - 1', window.jQuery);
    setTimeout(() => {
      console.log('settings click event now - 2', window.jQuery);
    }, 0);
    // jQuery('#btn').click(function() {
    //   alert('Hello!');
    // });
  }
);
