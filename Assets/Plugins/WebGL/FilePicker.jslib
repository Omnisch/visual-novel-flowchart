var FilePicker = {
	OpenHTMLFilePicker: function () {
		document.getElementById('hiddenFileInput').click();
	}
}

mergeInto(LibraryManager.library, FilePicker);
