import { renderMempool } from "./mempool.js";

const transactionForm = document.querySelector('.transaction-form');
const sendTransactionButton = document.getElementById('send-transaction');

sendTransactionButton.addEventListener('click', async (e) => {
    e.preventDefault();

    const senderInput = document.getElementById('sender');
    const receiverInput = document.getElementById('receiver');
    const amountInput = document.getElementById('amount');

    const senderName = senderInput.value.trim();
    const receiverName = receiverInput.value.trim();
    const amount = parseFloat(amountInput.value);

    if (!senderName || !receiverName || !amount) {
        alert('Please fill in all fields');
        return;
    }

    if (amount <= 0) {
        alert('Amount must be greater than 0');
        return;
    }

    try {
        const response = await fetch('http://localhost:5000/api/v1.0/transaction/new', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                senderWalletName: senderName,
                recipientWalletName: receiverName,
                amount: amount
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        senderInput.value = '';
        receiverInput.value = '';
        amountInput.value = '';
        alert('Transaction created successfully!');
        await renderMempool();

    } catch (error) {
        console.error('Error creating transaction:', error);
        alert('Failed to create transaction. Please try again.');
    }
});