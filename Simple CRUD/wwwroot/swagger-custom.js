(function (){
  // Add an Authorize input to Swagger UI by adding a simple token input and applying the header
  function addTokenInput(){
    var container = document.createElement('div');
    container.style.margin = '10px';
    container.innerHTML = '\n      <label style="margin-right:8px">JWT Token:</label>\n      <input id="custom-swagger-token" placeholder="Paste token here" style="width:400px" />\n      <button id="custom-swagger-set">Set Token</button>\n      <button id="custom-swagger-clear">Clear</button>\n    ';
    var el = document.querySelector('.swagger-ui');
    if(el) el.insertBefore(container, el.firstChild);

    document.getElementById('custom-swagger-set').addEventListener('click', function(){
      var t = document.getElementById('custom-swagger-token').value.trim();
      if(!t) return;
      window.localStorage.setItem('swagger_custom_token', t);
      setAuthHeader(t);
      alert('Token set');
    });

    document.getElementById('custom-swagger-clear').addEventListener('click', function(){
      window.localStorage.removeItem('swagger_custom_token');
      setAuthHeader(null);
      alert('Token cleared');
    });

    // apply existing token if present
    var existing = window.localStorage.getItem('swagger_custom_token');
    if(existing){
      document.getElementById('custom-swagger-token').value = existing;
      setAuthHeader(existing);
    }
  }

  function setAuthHeader(token){
    if(token){
      if(!window.__swagger_fetch_patched){
        var originalFetch = window.fetch;
        window.fetch = function(resource, init){
          init = init || {};
          init.headers = init.headers || {};
          init.headers['Authorization'] = 'Bearer ' + (window.swaggerUIAuthToken || '');
          return originalFetch(resource, init);
        };
        window.__swagger_fetch_patched = true;
      }
      window.swaggerUIAuthToken = token;
    } else {
      window.swaggerUIAuthToken = null;
    }
  }

  // wait for DOM
  document.addEventListener('DOMContentLoaded', function(){
    setTimeout(addTokenInput, 500);
  });
})();