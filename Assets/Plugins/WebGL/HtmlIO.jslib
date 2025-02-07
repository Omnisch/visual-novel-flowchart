var HtmlIO = {
	HtmlOpenFilePicker: function () {
		document.getElementById('hiddenFileInput').click();
	},
	HtmlSaveFile: function(fileName, dataPtr, dataLength) {
		var heap = Module.HEAPU8;
		var arrayBuffer = new ArrayBuffer(dataLength);
        var uint8Array = new Uint8Array(arrayBuffer);
		for (var i = 0; i < dataLength; i++) {
			uint8Array[i] = heap[dataPtr + i];
		}
		
        var blob = new Blob([uint8Array], { type: "application/octet-stream" });
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = UTF8ToString(fileName);
        link.click();
    }
}
mergeInto(LibraryManager.library, HtmlIO);
