CKEDITOR.plugins.add('bordercontent', {
    icons: 'bordercontent',
    init: function (editor) {
        editor.addCommand('insertBorderContent', {
            exec: function (editor) {
                var content = '';
                content += '<div class="border-content">';
                content += '<p>❝Nội dung❞</p>';
                content += '</div>';

                editor.insertHtml(content);
            }
        });
        editor.ui.addButton('BorderContent', {
            label: 'Nội dung có viền',
            command: 'insertBorderContent',
            toolbar: 'insert'
        });
    }
});
