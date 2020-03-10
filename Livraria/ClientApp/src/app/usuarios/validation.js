"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Validation = /** @class */ (function () {
    function Validation() {
    }
    Validation.ValidaCpf = function (controle) {
        var cpf = controle.value;
        var soma = 0;
        var resto;
        var valido;
        var regex = new RegExp('[0-9]{11}');
        if (cpf == '00000000000' ||
            cpf == '11111111111' ||
            cpf == '22222222222' ||
            cpf == '33333333333' ||
            cpf == '44444444444' ||
            cpf == '55555555555' ||
            cpf == '66666666666' ||
            cpf == '77777777777' ||
            cpf == '88888888888' ||
            cpf == '99999999999' ||
            !regex.test(cpf))
            valido = false;
        else {
            for (var i = 1; i <= 9; i++)
                soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
            resto = (soma * 10) % 11;
            if (resto == 10 || resto == 11)
                resto = 0;
            if (resto != parseInt(cpf.substring(9, 10)))
                valido = false;
            soma = 0;
            for (var i = 1; i <= 10; i++)
                soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
            resto = (soma * 10) % 11;
            if (resto == 10 || resto == 11)
                resto = 0;
            if (resto != parseInt(cpf.substring(10, 11)))
                valido = false;
            valido = true;
        }
        if (valido)
            return null;
        return { cpfInvalido: true };
    };
    Validation.ValidaTelefoneOuEmail = function (controle) {
        var telefone = controle.get('telefone').value;
        var email = controle.get('email').value;
        if (email || telefone)
            return null;
        controle.get('telefone').setErrors({ vazio: true });
        controle.get('email').setErrors({ vazio: true });
    };
    return Validation;
}());
exports.Validation = Validation;
//# sourceMappingURL=validation.js.map