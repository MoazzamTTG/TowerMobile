mergeInto(LibraryManager.library, {
  OnSelected: function(id) {
	 id = UTF8ToString(id);

	 if (window.MyNamespace.tower) {
		window.MyNamespace.tower.onSelected(id);
	 }
  },

  Get: function (key) {
    console.log("key: ", key);
    key = UTF8ToString(key);
    console.log("key: ", key);
	if (window.MyNamespace.tower[key]) {
		var data = JSON.stringify(window.MyNamespace.tower[key]);
   console.log("data: ", data);
		var buffer = _malloc(lengthBytesUTF8(data) + 1);
		writeStringToMemory(data, buffer);

     console.log("buffer: ", buffer);
		return buffer;
	}

  console.log("Empty---", key);

	return "";
  },

  IsMobile: function() {
        return new RegExp("iPhone|iPad|iPod|Android").test(navigator.userAgent);
  },

  Loaded: function () {
    try {
      window.dispatchReactUnityEvent("Loaded");
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },

});