// Write your JavaScript code.
/*Arrow Function para Tela Index da controladora Conta  */
const NovaConta = () => {
    window.location.href="../Conta/NovaConta"

}

const ExcluirConta = (id) => {
    const con = confirm("Tem certeza deseja Excluir?")
    if (con === true) {
        window.location.href = `../Conta/ExcluirConta/${id}`
    }
}


 //Arrow Function para as Telas Plano Contas

const NovoPlanoConta = () => {
    window.location.href="../PlanoConta/NovoPlanoConta"
}

const ExcluirPlanoConta = (id) => {

    const con = confirm("Tem certeza que deseja Excluir?")
    if (con) {
        window.location.href = `../PlanoConta/ExcluirPlanoConta/${id}`
    }


}

const EditarPlanoConta = (id) => {
    window.location.href =`../PlanoConta/NovoPlanoConta/${id}`
}

/* Tela Transação*/

const NovaTransacao = () => {
    window.location.href="../Transacao/NovaTransacao"
}

const EditarTransacao = (id) => {
    window.location.href = `../Transacao/NovaTransacao/${id}`
}
const ExcluirTransacao = (id) => {
    const conf = confirm("Tem certeza que deseja excluir esse essa Transação");
    window.location.href = `../Transacao/ExcluirTransacao/${id}`
}
const formCad = () => {
    window.location.href =`../Usuario/Cadastrar`
}

const formLogin = () => {
    window.location.href=`../Usuario/Login`
}