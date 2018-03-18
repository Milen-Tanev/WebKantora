window.onload = function () {
    var btn = $('#add-keyword');
    btn.on('click', function () {
        
        $('.all-keywords').on('click', '.remove-keyword', function () {
            var className = $(this).closest('li').attr('class');
            var keywords = $('.' + className);
            keywords.remove();
        });
    })
}() 
