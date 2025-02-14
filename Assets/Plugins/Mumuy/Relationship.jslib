mergeInto(LibraryManager.library, {
  Relationship: function(queryPtr) {
      var query = UTF8ToString(queryPtr);

      if (typeof relationship !== "undefined") {
          var resultArray = relationship({ text: query });
          if (Array.isArray(resultArray)) {
            var resultStr = JSON.stringify(resultArray);
            var buffer = allocate(intArrayFromString(resultStr), 'i8', ALLOC_NORMAL);
            return buffer;
          }
      }
      return allocate(intArrayFromString("Error"), 'i8', ALLOC_NORMAL);
  }
});
