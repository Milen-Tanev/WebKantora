function autocompleteKeywords(allKeywords) {
    window.onload = function () {
        let keywordInput = $('#keyword');
        keywordInput.autocomplete({
            source: allKeywords
        });
    };
}
